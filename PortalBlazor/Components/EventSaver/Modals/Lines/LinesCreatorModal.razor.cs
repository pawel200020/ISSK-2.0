using AutoMapper;
using BlazorBootstrap;
using EventsSaver.Shared.Managers.Seasons;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ViewModels.RazorPages.EventSaver.Lines;
using ViewModels.RazorPages.EventSaver.Seasons;

namespace PortalBlazor.Components.EventSaver.Modals.Lines;

public partial class LinesCreatorModal : ComponentBase
{
    [Inject] ISeasonsGetter SeasonsGetter { get; set; } = null!;
    [Inject] IMapper Mapper { get; set; } = null!;
    protected override void OnInitialized()
    {
        _editContext = new EditContext(Line);
        base.OnInitialized();
    }
    private Task HandleOnSubmit(EditContext arg)
    {
        Console.Write(Line.Number);
        throw new NotImplementedException();
    }

    private Task Close()
    {
        throw new NotImplementedException();
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
}