using AutoMapper;
using BlazorBootstrap;
using EventsSaver.Shared.Managers.Seasons;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Resources.PortalResources;
using ViewModels.RazorPages.EventSaver.Lines;
using ViewModels.RazorPages.EventSaver.Lines.Brigades;
using ViewModels.RazorPages.EventSaver.Seasons;
using ViewModels.RazorPages.Users;

namespace PortalBlazor.Components.EventSaver.Modals.Lines;

public partial class LinesCreatorModal : ComponentBase
{
    private EditContext? _editContext;
    private ValidationMessageStore? _brigadeValidationMessages;
    private Modal _modal = default!;

    [Parameter] public required LineViewModel Line { get; set; }
    [Parameter] public required Func<Task> OnCloseCallback { get; set; }
    [Inject] ISeasonsGetter SeasonsGetter { get; set; } = null!;
    [Inject] IMapper Mapper { get; set; } = null!;

    protected override void OnInitialized()
    {
        CreateEditContext();
        base.OnInitialized();
    }

    public override Task SetParametersAsync(ParameterView parameters)
    {
        if (parameters.TryGetValue<LineViewModel>(nameof(Line), out var newLine) && newLine != Line)
        {
            Line = newLine;
            CreateEditContext();
        }
        return base.SetParametersAsync(parameters);
    }

    private void CreateEditContext()
    {
        _editContext = new EditContext(Line);
        _brigadeValidationMessages = new ValidationMessageStore(_editContext);
        _editContext.OnValidationRequested += ValidateBrigades;
        _editContext.OnFieldChanged += ValidateBrigadeField;
    }

    private void ValidateBrigades(object? sender, ValidationRequestedEventArgs args)
    {
        if (_brigadeValidationMessages is null)
            return;

        _brigadeValidationMessages.Clear();

        if (Line.Brigades is null || Line.Brigades.Count == 0)
        {
            _brigadeValidationMessages.Add(
                new FieldIdentifier(Line, nameof(Line.Brigades)),
                PortalResources.cAtLeastOneBrigadeRequired);
            return;
        }

        foreach (var brigade in Line.Brigades)
        {
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(brigade, new ValidationContext(brigade), validationResults, true);

            foreach (var validationResult in validationResults)
            {
                foreach (var memberName in validationResult.MemberNames)
                {
                    _brigadeValidationMessages.Add(
                        new FieldIdentifier(brigade, memberName),
                        validationResult.ErrorMessage ?? string.Empty);
                }
            }
        }

    }

    private void ValidateBrigadeField(object? sender, FieldChangedEventArgs args)
    {
        if (_brigadeValidationMessages is null ||
            args.FieldIdentifier.Model is not BrigadeViewModel brigade)
            return;

        _brigadeValidationMessages.Clear(args.FieldIdentifier);

        var property = brigade.GetType().GetProperty(args.FieldIdentifier.FieldName);
        if (property is null)
            return;

        var validationResults = new List<ValidationResult>();
        Validator.TryValidateProperty(
            property.GetValue(brigade),
            new ValidationContext(brigade) { MemberName = args.FieldIdentifier.FieldName },
            validationResults);

        foreach (var validationResult in validationResults)
        {
            _brigadeValidationMessages.Add(
                args.FieldIdentifier,
                validationResult.ErrorMessage ?? string.Empty);
        }

        _editContext?.NotifyValidationStateChanged();
    }

    private async Task HandleOnSubmit()
    {
        Console.Write(Line.Number);
        await Task.CompletedTask;
    }

    private async Task<AutoCompleteDataProviderResult<SeasonViewModel>> CustomersDataProvider(AutoCompleteDataProviderRequest<LineViewModel> request)
    {
        var seasons = (await SeasonsGetter.SearchSeason(request.Filter.Value))!
            .Select(s => Mapper.Map<SeasonViewModel>(s));
        return await Task.FromResult(new AutoCompleteDataProviderResult<SeasonViewModel> { Data = seasons, TotalCount = 5});
    }

    public async Task AddBrigade()
    {
        Line.Brigades ??= new List<BrigadeViewModel>();

        Line.Brigades.Add(new BrigadeViewModel
        {
            Id = Guid.NewGuid().ToString(),
            Name = string.Empty,
            StartHour = new TimeOnly(9,0),
            EndHour = new TimeOnly(17,0),
            Points = 1,
            Capacity = 4,
            IsActive = true,
            People = new List<UserMetadataViewModel>()
        });

        ClearBrigadeCountValidation();
        await InvokeAsync(StateHasChanged);
    }

    private void RemoveBrigade(BrigadeViewModel brigade)
    {
        Line.Brigades?.Remove(brigade);
        ClearBrigadeCountValidation();
    }

    private void ClearBrigadeCountValidation()
    {
        if (_editContext is null || _brigadeValidationMessages is null)
            return;

        _brigadeValidationMessages.Clear(new FieldIdentifier(Line, nameof(Line.Brigades)));
        _editContext.NotifyValidationStateChanged();
    }

    private async Task EditDetails(BrigadeViewModel brigade)
    {
        var parameters = new Dictionary<string, object>() { { "Brigade", brigade} };
        await _modal.ShowAsync<BrigadeDetailsModal>(title: PortalResources.cBrigadeDetails, parameters: parameters);
    }
}