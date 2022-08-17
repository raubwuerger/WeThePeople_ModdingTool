using NUnit.Framework;
using System.Collections.Generic;
using WeThePeople_ModdingTool;
using WeThePeople_ModdingTool.FileUtilities;

namespace WeThePeople_TestProject
{
    public class TextReplacer_Tests
    {
        string replaceItem_Placeholder = "$TO_REPLACE$";
        string replaceItem_PlaceholderNotMatching = "$NOT_MATCHING$";
        string replaceItem_Value = "world!";
        string contentWithoutPlaceholder = "Nothing to replace ...";
        string contentWithwrongPlaceholder = "Hello $IM_WRONG$";
        string contentValid = "Hello $TO_REPLACE$";
        string contentValidReplaced = "Hello world!";

        KeyValuePair<string, string> keyValuePair_null;
        KeyValuePair<string, string> keyVakuePair_valid;
        KeyValuePair<string, string> keyValuePair_bob;
        KeyValuePair<string, string> keyValuePair_eve;

        string contentValidManyPlaceholders = "Hello $TO_REPLACE$ We are $BOB$ and $EVE$!";
        string contentValidManyPlaceholdersReplaced = "Hello world! We are bob and eve!";

        IDictionary<string, string> replaceItemsNull;
        IDictionary<string, string> replaceItemsEmpty;
        IDictionary<string, string> replaceItemsOne;
        IDictionary<string, string> replaceItemsMany;

        [SetUp]
        public void Setup()
        {
            CommonMessageBox.ShowMessageBoxes = false;

            keyVakuePair_valid = new KeyValuePair<string, string>(replaceItem_Placeholder, replaceItem_Value);
            keyValuePair_bob = new KeyValuePair<string, string>("$BOB$", "bob");
            keyValuePair_eve = new KeyValuePair<string, string>("$EVE$", "eve");

            replaceItemsEmpty = new Dictionary<string, string>();

            replaceItemsOne = new Dictionary<string, string>();
            replaceItemsOne.Add(keyVakuePair_valid);

            replaceItemsMany = new Dictionary<string, string>();
            replaceItemsMany.Add(keyVakuePair_valid);
            replaceItemsMany.Add(keyValuePair_bob);
            replaceItemsMany.Add(keyValuePair_eve);
        }

        [Test]
        public void ContentNull_ReplaceItemNull_Test()
        {
            Assert.IsNull( TextReplacer.Replace(null, keyValuePair_null) );
        }

        [Test]
        public void ContentEmpty_ReplaceItemNull_Test()
        {
            Assert.IsNull(TextReplacer.Replace("", keyValuePair_null) );
        }

        [Test]
        public void ContentEmpty_ReplaceItemEmpty_Test()
        {
            Assert.IsNull(TextReplacer.Replace("", new KeyValuePair<string, string>("", "")));
        }

        [Test]
        public void ContentWithoutPlaceholder_ReplaceItemValid_Test()
        {
            Assert.AreNotEqual(contentValidReplaced, TextReplacer.Replace(contentWithoutPlaceholder, new KeyValuePair<string, string>(replaceItem_Placeholder, replaceItem_Value)));
        }

        [Test]
        public void ContentWithWrongPlaceholder_ReplaceItemValid_Test()
        {
            Assert.AreNotEqual(contentValidReplaced, TextReplacer.Replace(contentWithwrongPlaceholder, new KeyValuePair<string, string>(replaceItem_Placeholder, replaceItem_Value)));
        }

        [Test]
        public void ContentValid_ReplaceItemNotMatching_Test()
        {
            Assert.AreNotEqual(contentValidReplaced, TextReplacer.Replace(contentValid, new KeyValuePair<string, string>(replaceItem_PlaceholderNotMatching, replaceItem_Value)));
        }

        [Test]
        public void ContentValid_ReplaceItemValid_Test()
        {
            Assert.AreEqual(contentValidReplaced, TextReplacer.Replace(contentValid, new KeyValuePair<string, string>(replaceItem_Placeholder, replaceItem_Value)));
        }

        [Test]
        public void ContentValid_ReplaceItemsValid_Test()
        {
            Assert.AreEqual(contentValidManyPlaceholdersReplaced, TextReplacer.Replace(contentValidManyPlaceholders, replaceItemsMany));
        }
    }
}