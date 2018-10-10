namespace MyTelescope.App.Utilities.Models
{
    using Enums;

    public class DrawParameterModel
    {
        public DrawParameterModel(ObjectCollectionLayout objectCollectionLayout, DrawOrientation orientation, int centreX, int centreY, int orbitFactor)
        {
            ObjectCollectionLayout = objectCollectionLayout;
            Orientation = orientation;
            CentreX = centreX;
            CentreY = centreY;
            OrbitFactor = orbitFactor;
        }

        public ObjectCollectionLayout ObjectCollectionLayout { get; }

        public DrawOrientation Orientation{ get; }
        
        public int CentreX { get; }

        public int CentreY { get; }

        public int OrbitFactor { get; }
    }
}
