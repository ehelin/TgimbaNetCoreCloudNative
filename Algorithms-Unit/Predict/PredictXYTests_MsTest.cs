using Algorithms.Algorithms.Predict;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithms.Algorithms.Predict.Implementations;

namespace Algorithms_Unit
{
    [TestClass]
    public class PredictXYTests_MsTest
    {
        private readonly IPredict sut;

        public PredictXYTests_MsTest()
        {
            sut = new PredictXY();
        }
    
        [TestMethod]
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
