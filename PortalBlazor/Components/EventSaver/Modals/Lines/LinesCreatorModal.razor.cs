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

    [Parameter] public LineViewModel Line { get; set; } = new LineViewModel { Id = Guid.NewGuid().ToString(), };

    [Inject] ISeasonsGetter SeasonsGetter { get; set; } = null!;
    [Inject] IMapper Mapper { get; set; } = null!;

    protected override void OnInitialized()
    {
        _editContext = new EditContext(Line);
        base.OnInitialized();
    }

    public override Task SetParametersAsync(ParameterView parameters)
    {
        // ensure edit context is recreated when parameter changes
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

    private Task Close()
    {
        return Task.CompletedTask;
    }

    private async Task<AutoCompleteDataProviderResult<SeasonViewModel>> CustomersDataProvider(AutoCompleteDataProviderRequest<LineViewModel> request)
    {
        var seasons = (await SeasonsGetter.SearchSeason(request.Filter.Value))!
            .Select(s => Mapper.Map<SeasonViewModel>(s));
        return await Task.FromResult(new AutoCompleteDataProviderResult<SeasonViewModel> { Data = seasons, TotalCount = 5});
    }

    private async Task<AutoCompleteDataProviderResult<SeasonViewModel>> SeasonDataProvider(AutoCompleteDataProviderRequest<SeasonViewModel> request)
    {
        var seasons = (await SeasonsGetter.SearchSeason(request.Filter.Value))!
            .Select(s => Mapper.Map<SeasonViewModel>(s));
        return await Task.FromResult(new AutoCompleteDataProviderResult<SeasonViewModel> { Data = seasons, TotalCount = seasons.Count()});
    }

    private Task<GridDataProviderResult<BrigadeViewModel>> BrigadeProvider(GridDataProviderRequest<BrigadeViewModel> request)
    {
        return Task.FromResult(new GridDataProviderResult<BrigadeViewModel> { Data = new List<BrigadeViewModel>(), TotalCount = 0 });
    }

    public async Task AddBrigade()
    {
        if (Line.Brigades == null) Line.Brigades = new List<BrigadeViewModel>();

        Line.Brigades.Add(new BrigadeViewModel
        {
            Id = Guid.NewGuid().ToString(),
            Name = string.Empty,
            StartHour = new TimeOnly(0,0),
            EndHour = new TimeOnly(0,0),
            Points = 0,
            People = new List<UserMetadataViewModel>()
        });

        await InvokeAsync(StateHasChanged);
    }

    public Task RemoveBrigade(BrigadeViewModel brigade)
    {
        if (Line.Brigades != null)
            Line.Brigades.Remove(brigade);
        return Task.CompletedTask;
    }

    public void OnStartHourChanged(BrigadeViewModel? brigade, string? value)
    {
        if (brigade is null || string.IsNullOrWhiteSpace(value)) return;
        if (TimeOnly.TryParse(value, out var t))
        {
            brigade.StartHour = t;
        }
    }

    public void OnEndHourChanged(BrigadeViewModel brigade, string? value)
    {
        if (brigade is null || string.IsNullOrWhiteSpace(value)) return;
        if (TimeOnly.TryParse(value, out var t))
        {
            brigade.EndHour = t;
        }
    }

    private async Task EditDetails(BrigadeViewModel brigade)
    {
        
        var parameters = new Dictionary<string, object>()
        {
            { "Brigade", brigade}
        };
        await _modal.ShowAsync<BrigadeDetailsModal>(title: PortalResources.cLine, parameters: parameters);
    }
}