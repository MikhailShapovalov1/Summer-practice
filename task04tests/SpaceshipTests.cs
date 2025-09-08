using task04;

namespace task04tests
{
    public class SpaceshipTests
    {
        [Fact]
        public void Cruiser_ShouldHaveCorrectStats()
        {
            ISpaceship cruiser = new Cruiser();
            Assert.Equal(50, cruiser.Speed);
            Assert.Equal(100, cruiser.FirePower);
        }

        [Fact]
        public void Fighter_ShouldHaveCorrectStats()
        {
            ISpaceship fighter = new Fighter();
            Assert.Equal(100, fighter.Speed);
            Assert.Equal(50, fighter.FirePower);
        }

        [Fact]
        public void Fighter_ShouldBeFasterThanCruiser()
        {
            var fighter = new Fighter();
            var cruiser = new Cruiser();
            Assert.True(fighter.Speed > cruiser.Speed);
        }

        [Fact]
        public void Cruiser_ShouldBePowerfulThanFighter()
        {
            var fighter = new Fighter();
            var cruiser = new Cruiser();
            Assert.True(cruiser.FirePower > fighter.FirePower);
        }

        [Fact]
        public void SpaceshipsMove_ShouldBeCorrectDistance()
        {
            var fighter = new Fighter();
            var cruiser = new Cruiser();

            fighter.MoveForward();
            cruiser.MoveForward();

            Assert.Equal(100, fighter.TotalDistance);
            Assert.Equal(50, cruiser.TotalDistance);
        }

        [Fact]
        public void SpaceshipsFire_ShouldBeCorrectDamage()
        {
            var fighter = new Fighter();
            var cruiser = new Cruiser();

            fighter.Fire();
            cruiser.Fire();

            Assert.Equal(50, fighter.TotalDamage);
            Assert.Equal(100, cruiser.TotalDamage);
        }

        [Fact]
        public void SpaceshipsRotation_ShouldBeCorrectRotationAngle()
        {
            var fighter = new Fighter();
            var cruiser = new Cruiser();

            fighter.Rotate(20);
            cruiser.Rotate(400);

            Assert.Equal(20, fighter.RotationAngle);
            Assert.Equal(40, cruiser.RotationAngle);
        }
    }
}
