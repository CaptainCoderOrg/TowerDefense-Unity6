using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GameStatsTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void GameStatsTestsSimplePasses()
    {
        Assert.AreEqual("0:00", GameStats.SecondsToDisplayTime(0));
        Assert.AreEqual("0:31", GameStats.SecondsToDisplayTime(31));
        Assert.AreEqual("1:00", GameStats.SecondsToDisplayTime(60));
        Assert.AreEqual("1:37", GameStats.SecondsToDisplayTime(97));
        Assert.AreEqual("5:00", GameStats.SecondsToDisplayTime(300));
        Assert.AreEqual("60:00", GameStats.SecondsToDisplayTime(60 * 60));
    }
}
