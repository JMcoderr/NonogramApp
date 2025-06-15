using Microsoft.VisualStudio.TestTools.UnitTesting;
using NonogramApp.Utility;

namespace NonogramTest.Unit;

[TestClass]
public class PassHasherTest
{
    [TestMethod]
    //This checks if the hash made is consitent and gives back the same value when the input is the same. This is crucial for verification
    public void ReturnSameHash()
    {
        uint hash1 = Hash.PassHasher("secret");
        uint hash2 = Hash.PassHasher("secret");
        Assert.AreEqual(hash1, hash2);
    }
}
