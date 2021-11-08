using UnityEngine.EventSystems;

public class ClickableUnit : BaseUnit, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData) => InteractionHandler((int)(_time / _liveTime * _possibleScorePoints));
}
