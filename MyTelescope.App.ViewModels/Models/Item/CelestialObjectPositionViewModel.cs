namespace MyTelescope.App.ViewModels.Models.Item
{
    using SolarSystem.Models.CelestialObject;

    public class CelestialObjectPositionViewModel : BaseKeyViewModel<CelestialObjectPositionModel>
    {
        public CelestialObjectPositionViewModel()
        {
        }

        public CelestialObjectPositionViewModel(CelestialObjectPositionModel model)
            : base(model)
        {
        }

        public double RatioEarthAuDistance
        {
            get => Model.RatioEarthAuDistance;
            set => Model.RatioEarthAuDistance = value;
        }

        public double RatioSunEarthDistance
        {
            get => Model.RatioSunEarthDistance;
            set => Model.RatioSunEarthDistance = value;
        }

        public double LargeDeltaEarthDistance
        {
            get => Model.LargeDeltaEarthDistance;
            set => Model.LargeDeltaEarthDistance = value;
        }

        public double CentricDistance
        {
            get => Model.CentricDistance;
            set => Model.CentricDistance = value;
        }
    }
}