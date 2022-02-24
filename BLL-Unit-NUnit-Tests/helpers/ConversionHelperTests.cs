using System;
using BLLNetCore.helpers;
using NUnit.Framework;
using Shared.interfaces;

namespace TestAPINetCore_Unit.helpers
{
    [NonParallelizable]
    public class ConversionHelperTests : BaseTest
    {
        private IConversion sut = null;

        public ConversionHelperTests() {
            sut = new ConversionHelper();
        }

        #region GetSafeBool(val)

        [Test]
        public void GetSafeBool_ActualTrue()
        {
            object val = true;
            var result = sut.GetSafeBool(val);
            Assert.IsTrue(result);
        }

        [Test]
        public void GetSafeBool_ImpliedTrue()
        {
            object val = 1;
            var result = sut.GetSafeBool(val);
            Assert.IsTrue(result);
        }

        [Test]
        public void GetSafeBool_ActualFalse()
        {
            object val = false;
            var result = sut.GetSafeBool(val);
            Assert.IsFalse(result);
        }

        [Test]
        public void GetSafeBool_ImpliedFalse()
        {
            object val = 0;
            var result = sut.GetSafeBool(val);
            Assert.IsFalse(result);
        }

        [Test]
        public void GetSafeBool_NullFalse()
        {
            object val = null;
            var result = sut.GetSafeBool(val);
            Assert.IsFalse(result);
        }

        [Test]
        public void GetSafeBool_DbNullFalse()
        {
            object val = DBNull.Value;
            var result = sut.GetSafeBool(val);
            Assert.IsFalse(result);
        }

        #endregion

        #region GetSafeDecimal(val)

        [Test]
        public void GetSafeDecimal_HappyPath()
        {
            object val = 1.1;
            var result = sut.GetSafeDecimal(val);
            Assert.IsTrue(result == (decimal)1.1);
        }

        [Test]
        public void GetSafeDecimal_IntToDecimal()
        {
            object val = 1;
            var result = sut.GetSafeDecimal(val);
            Assert.IsTrue(result == (decimal)1);
        }

        [Test]
        public void GetSafeDecimal_Null()
        {
            object val = null;
            var result = sut.GetSafeDecimal(val);
            Assert.IsTrue(result == (decimal)0);
        }

        [Test]
        public void GetSafeDecimal_DbNull()
        {
            object val = DBNull.Value;
            var result = sut.GetSafeDecimal(val);
            Assert.IsTrue(result == (decimal)0);
        }

        #endregion

        #region GetSafeInt(val)

        [Test]
        public void GetSafeInt_HappyPath()
        {
            object val = 1;
            var result = sut.GetSafeInt(val);
            Assert.IsTrue(result == 1);
        }

        [Test]
        public void GetSafeInt_DecimalToInt()
        {
            object val = 1.1;
            var result = sut.GetSafeInt(val);
            Assert.IsTrue(result == 1);
        }
        
        [Test]
        public void GetSafeInt_Null()
        {
            object val = null;
            var result = sut.GetSafeInt(val);
            Assert.IsTrue(result == 0);
        }

        [Test]
        public void GetSafeInt_DbNull()
        {
            object val = DBNull.Value;
            var result = sut.GetSafeInt(val);
            Assert.IsTrue(result == 0);
        }

        [Test]
        public void GetSafeInt_EmptyString()
        {
            object val = "";
            var result = sut.GetSafeInt(val);
            Assert.IsTrue(result == 0);
        }

        #endregion

        #region GetSafeString(val)

        [Test]
        public void GetSafeString_HappyIntToString()
        {
            object val = 1;
            var result = sut.GetSafeString(val);
            Assert.IsTrue(result == "1");
        }

        [Test]
        public void GetSafeString_DecimalToString()
        {
            object val = 1.1;
            var result = sut.GetSafeString(val);
            Assert.IsTrue(result == "1.1");
        }

        [Test]
        public void GetSafeString_Null()
        {
            object val = null;
            var result = sut.GetSafeString(val);
            Assert.IsTrue(result == String.Empty);
        }

        [Test]
        public void GetSafeString_DbNull()
        {
            object val = DBNull.Value;
            var result = sut.GetSafeString(val);
            Assert.IsTrue(result == String.Empty);
        }

        #endregion
        
        #region GetSafeDateTime(val)

        [Test]
        public void GetSafeDateTime_HappyPath()
        {
            object val = "1/2/2020";
            var result = sut.GetSafeDateTime(val);
            Assert.IsTrue(result == DateTime.Parse(val.ToString()));
        }

        [Test]
        public void GetSafeDateTime_Null()
        {
            object val = null;
            var result = sut.GetSafeDateTime(val);
            Assert.IsTrue(result == DateTime.MinValue);
        }

        [Test]
        public void GetSafeDateTime_DbNull()
        {
            object val = DBNull.Value;
            var result = sut.GetSafeDateTime(val);
            Assert.IsTrue(result == DateTime.MinValue);
        }

        #endregion
    }
}
