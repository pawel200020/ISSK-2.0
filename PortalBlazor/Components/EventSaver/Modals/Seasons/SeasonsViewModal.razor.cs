using AutoMapper;
using BlazorBootstrap;
using EventsSaver.Shared.Managers.Seasons;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using PortalBlazor.Toasts;
using Resources.PortalResources;
using ViewModels.RazorPages.EventSaver.Seasons;

namespace PortalBlazor.Components.EventSaver.Modals.Seasons;

public partial class SeasonsViewModal : ComponentBase
{
    [Inject] private ISeasonsGetter SeasonsGetter { get; set; } = null!;
    [Inject] private IMapper Mapper { get; set; } = null!;
    [Inject] private ToastMessageCreationService ToastMessageCreationService { get; set; } = null!;
    [Inject] private ILogger<SeasonsViewModal> Logger { get; set; } = null!;
    [Inject] private ISeasonRemover SeasonRemover { get; set; } = null!;

    private async Task<GridDataProviderResult<SeasonViewModel>> ReadData(
        GridDataProviderRequest<SeasonViewModel> request)
    {
        StateHasChanged();
        var seasonsPaged = await SeasonsGetter.GetSeasonsPaged(request.PageNumber, request.PageSize);
        var data = seasonsPaged?.Select(s => Mapper.Map<SeasonViewModel>(s)) ?? Enumerable.Empty<SeasonViewModel>();
        var totalCount = await SeasonsGetter.GetSeasonsCount();
        return new GridDataProviderResult<SeasonViewModel> { Data = data, TotalCount = totalCount };
    }

    private async Task CreateNewSeason()
    {
        var parameters = new Dictionary<string, object>
        {
            { "OnSuccessCallback", EventCallback.Factory.Create<Task>(this, OnSuccessCreateSeason) },
            { "OnFailCallBack", EventCallback.Factory.Create<Task>(this, OnFail) },
            {
                "Season", new SeasonViewModel { SeasonId = Guid.NewGuid().ToString(), Name = string.Empty }
            }
        };

        await _modal.ShowAsync<SeasonCreatorModal>(title: PortalResources.cAddSeason, parameters: parameters);
    }

    private async Task OnSuccessCreateSeason()
    {
        await _modal.HideAsync();
        await _grid.RefreshDataAsync();
        ShowToast(PortalResources.cSuccess, PortalResources.cSeasonAdded, ToastType.Success);
    }

    private async Task OnFail()
    {
        await _modal.HideAsync();
        await _grid.RefreshDataAsync();
        ShowFailToast();
    }

    private void ShowToast(string title, string message, ToastType toastType)
        => _messages.Add(ToastMessageCreationService.CreateToastMessage(title, message, toastType));

    private void ShowFailToast()
        => _messages.Add(ToastMessageCreationService.CreateUnknownErrorToastMessage());

    // helpers used by toast methods
    private readonly List<ToastMessage> _messages = new();

    private SeasonViewModel? _seasonToDelete;

    private async Task ConfirmDelete(SeasonViewModel season)
    {
        _seasonToDelete = season;

        var parameters = new Dictionary<string, object>
        {
            { "OnCloseClick", EventCallback.Factory.Create<MouseEventArgs>(this, OnCloseConfirm) },
            { "OnDeleteClick", EventCallback.Factory.Create<MouseEventArgs>(this, OnDeleteConfirm) }
        };

        await _deleteModal.
            ShowAsync<PortalBlazor.Components.Modals.ConfirmationModals.ConfirmDeleteModal>(
            title: PortalResources.cDelete, parameters: parameters);
    }

    private async Task OnCloseConfirm(MouseEventArgs e)
    {
        await _deleteModal.HideAsync();
        _seasonToDelete = null;
    }

    private async Task OnDeleteConfirm(MouseEventArgs e)
    {
        // hide confirmation modal
        await _modal.HideAsync();

        if (_seasonToDelete is null)
            return;

        try
        {
            await SeasonRemover.RemoveSeason(new Guid(_seasonToDelete.SeasonId));
            _seasonToDelete = null;
            await _deleteModal.HideAsync();
            await _grid.RefreshDataAsync();
            ShowToast(PortalResources.cSuccess, PortalResources.cSeasonRemoved, ToastType.Success);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Failed to remove season");
            ShowFailToast();
        }
    }
}