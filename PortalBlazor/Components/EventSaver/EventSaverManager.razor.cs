using AutoMapper;
using BlazorBootstrap;
using EventsSaver.Shared.Managers.Lines;
using Microsoft.AspNetCore.Components;
using PortalBlazor.Components.EventSaver.Modals;
using Resources.PortalResources;
using ViewModels.RazorPages.EventSaver.Lines;

namespace PortalBlazor.Components.EventSaver;

public partial class EventSaverManager : ComponentBase
{
    [Inject] private ILinesGetter LinesGetter { get; init; }
    [Inject] private IMapper Mapper { get; set; } = null!;
    
    private Task CreateNewEvent()
    {
        throw new NotImplementedException();
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