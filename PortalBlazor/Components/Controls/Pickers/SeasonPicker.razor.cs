using System.Linq.Expressions;
using AutoMapper;
using EventsSaver.Shared.Managers.Seasons;
using Microsoft.AspNetCore.Components;
using ViewModels.RazorPages.EventSaver.Seasons;

namespace PortalBlazor.Components.Controls.Pickers;

public partial class SeasonPicker : ComponentBase
{
    [Inject] ISeasonsGetter SeasonsGetter { get; set; } = null!;
    [Inject] IMapper Mapper { get; set; } = null!;
    [Parameter] public SeasonViewModel? Value { get; set; }
    [Parameter] public EventCallback<SeasonViewModel?> ValueChanged { get; set; }
    [Parameter] public Expression<Func<SeasonViewModel?>>? ValueExpression { get; set; }
    private async Task<IEnumerable<SeasonViewModel>> SeasonDataProvider(string searchPhrase)
    {
          return (await SeasonsGetter.SearchSeason(searchPhrase))!
         .Select(s => Mapper.Map<SeasonViewModel>(s));

    }

    private async Task OnValueChanged(SeasonViewModel? newValue)
    {
        Value = newValue;
        await ValueChanged.InvokeAsync(newValue);
    }
}