using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CCard;
using System.Collections.Generic;

namespace UnitTestProject1 {
    [TestClass]
    public class UnitTest1 {
        [TestMethod]
        public void TestMethod1() {
            var wallet1 = new {
                Expected = 16M,
                Cards = new[] {
                    new { Expected = 10M, Card = (SimpleCard)new Visa { Balance = 100 } },
                    new { Expected = 5M, Card = (SimpleCard)new MasterCard { Balance = 100 } },
                    new { Expected = 1M, Card = (SimpleCard)new Discover { Balance = 100 } }}
            };
            // total int per person/wallet
            Assert.AreEqual(wallet1.Expected, wallet1.Cards.Select(c => c.Card).SumInterests());
            // total int per card
            Assert.IsTrue(wallet1.Cards.All(c => c.Card.GetSimpleInterest() == c.Expected));
        }
        [TestMethod]
        public void TestMethod2() {
            var person1 = new {
                Expected = 16M,
                Wallets = new[] {
                    new {
                        Expected = 11M,
                        Cards = new SimpleCard[] {
                            new Visa { Balance = 100 },
                            new Discover { Balance = 100 } }},
                    new {
                        Expected = 5M,
                        Cards = new SimpleCard[] {
                            new MasterCard { Balance = 100 } }}
                }
            };
            // total int per person
            Assert.AreEqual(person1.Expected, person1.Wallets.SelectMany(w => w.Cards).SumInterests());
            // total int per wallet
            Assert.IsTrue(person1.Wallets.All(w => w.Expected.Equals(w.Cards.SumInterests())));
        }
        [TestMethod]
        public void TestMethod3() {
            var persons = new[] {
                 new {
                    Expected = 15M,
                    Wallets = new[] {
                        new {
                            Expected = 15M,
                            Cards = new SimpleCard[] {
                                new MasterCard { Balance = 100 },
                                new Visa { Balance = 100 } }}}
                 },
                 new {
                    Expected = 15M,
                    Wallets = new[] {
                        new {
                            Expected = 15M,
                            Cards = new SimpleCard[] {
                                new Visa { Balance = 100 },
                                new MasterCard { Balance = 100 } }}}
                 }
            };
            // total int per person
            Assert.IsTrue(persons.All(p => p.Expected.Equals(p.Wallets.SelectMany(w => w.Cards).SumInterests())));
            // total int per wallet
            Assert.IsTrue(persons.SelectMany(p => p.Wallets).All(w => w.Expected.Equals(w.Cards.SumInterests())));
        }
    }
    public static class Extension {
        public static decimal SumInterests(this IEnumerable<SimpleCard> cards) => cards.Sum(c => c.GetSimpleInterest());
    }
}