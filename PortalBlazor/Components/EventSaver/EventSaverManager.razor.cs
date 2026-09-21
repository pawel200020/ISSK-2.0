using AutoMapper;
using BlazorBootstrap;
using EventsSaver.Shared.Managers.Lines;
using Microsoft.AspNetCore.Components;
using PortalBlazor.Components.EventSaver.Modals;
using PortalBlazor.Components.EventSaver.Modals.Lines;
using PortalBlazor.Components.EventSaver.Modals.Seasons;
using Resources.PortalResources;
using ViewModels.RazorPages.EventSaver.Lines;

namespace PortalBlazor.Components.EventSaver;

public partial class EventSaverManager : ComponentBase
{
    private Modal _modal = default!;
    [Inject] private ILinesGetter LinesGetter { get; init; }
    [Inject] private IMapper Mapper { get; set; } = null!;
    
    private async Task CreateNewEvent()
    {
        await _modal.ShowAsync<LinesCreatorModal>(title: PortalResources.cLine, 
            parameters: new Dictionary<string, object> { { "OnCloseCallback", Close } });
        return;

        async Task Close() => await _modal.HideAsync();
    }

    private Task ExcelExport()
    {
        throw new NotImplementedException();
    }

    private async Task OpenSeasonModal()
    {
        await _modal.ShowAsync<SeasonsViewModal>(title: PortalResources.cSeason);
    }
    
    private async Task<GridDataProviderResult<LineViewModel>> ReadData(GridDataProviderRequest<LineViewModel> request)
    {
        StateHasChanged();
        var linesPaged = await LinesGetter.GetLinesPaged(request.PageNumber, request.PageSize);
        var data = linesPaged?.Select(s => Mapper.Map<LineViewModel>(s)) ?? Enumerable.Empty<LineViewModel>();
        var totalCount = await LinesGetter.GetLinesCount();
        return new GridDataProviderResult<LineViewModel> { Data = data, TotalCount = totalCount };
    }
}