using AutoMapper;
using BlazorBootstrap;
using EventsSaver.Shared.Managers.Seasons;
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
    private Modal _modal = default!;

    [Parameter] public required LineViewModel Line { get; set; }
    [Parameter] public required Func<Task> OnCloseCallback { get; set; }
    [Inject] ISeasonsGetter SeasonsGetter { get; set; } = null!;
    [Inject] IMapper Mapper { get; set; } = null!;

    protected override void OnInitialized()
    {
        _editContext = new EditContext(Line);
        base.OnInitialized();
    }

    public override Task SetParametersAsync(ParameterView parameters)
    {
        if (parameters.TryGetValue<LineViewModel>(nameof(Line), out var newLine) && newLine != Line)
        {
            Line = newLine;
            _editContext = new EditContext(Line);
        }
        return base.SetParametersAsync(parameters);
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

        await InvokeAsync(StateHasChanged);
    }

    private void RemoveBrigade(BrigadeViewModel brigade) 
        => Line.Brigades?.Remove(brigade);

    private async Task EditDetails(BrigadeViewModel brigade)
    {
        var parameters = new Dictionary<string, object>() { { "Brigade", brigade} };
        await _modal.ShowAsync<BrigadeDetailsModal>(title: PortalResources.cBrigadeDetails, parameters: parameters);
    }
}