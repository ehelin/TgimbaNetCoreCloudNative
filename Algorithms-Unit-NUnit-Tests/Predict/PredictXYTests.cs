using Algorithms.Algorithms.Predict;
using NUnit.Framework;
using Algorithms.Algorithms.Predict.Implementations;

namespace Algorithms_Unit
{
    public class PredictXYTests
    {
        private readonly IPredict sut;

        public PredictXYTests()
        {
            sut = new PredictXY();
        }
    
        [Test]
        [Ignore("implement")]
        public void PredictXY()
        {
            var x = 1;
            var y = 1;

            var result = sut.Predict(x, y);

            Assert.IsTrue(result > 0);
        }
    }
}
