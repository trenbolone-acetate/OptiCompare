using System.Text.RegularExpressions;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace OptiCompare.OptiCompareTests;
using Microsoft.Playwright;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class OptiCompareTests : PageTest
{
    [Test]
    public async Task HomeToPhonesListCheckFirstPhone()
    {
        await Page.GotoAsync("https://localhost:7107/");

        await Expect(Page).ToHaveTitleAsync(new Regex("OptiCompare"));

        await Page.GetByRole(AriaRole.Link, new() { NameString = "Phones" }).ClickAsync();

        var firstCellData = await Page.EvalOnSelectorAsync<string>("table > tbody > tr:first-child > th:first-child",
            "e => e.innerText");

        Assert.That(firstCellData, Is.EqualTo("Samsung Galaxy S24 5G"));
    }
    [Test]
    public async Task CheckAmountOfSamsungPhones()
    {
        await Page.GotoAsync("https://localhost:7107/");
    
        await Expect(Page).ToHaveTitleAsync(new Regex("OptiCompare"));
        
        await Page.GetByRole(AriaRole.Link, new() { NameString = "Phones" }).ClickAsync();

        var searchInput = await Page.QuerySelectorAsync(".search-create > .search-form > .form-control");
        if(searchInput != null)
            await searchInput.FillAsync("Samsung");
        
        var searchBttn = await Page.QuerySelectorAsync(".search-create > .search-form > input.btn");
        if(searchBttn != null)
            await searchBttn.ClickAsync();
        
        var rows = Page.GetByRole(AriaRole.Row);
        var count = await rows.CountAsync();
        //should be 7 but header row is also counted
        Assert.That(count,Is.EqualTo(8));
    }
    [Test]
    public async Task CheckGooglePixel7Details()
    {
        await Page.GotoAsync("https://localhost:7107/");
    
        await Expect(Page).ToHaveTitleAsync(new Regex("OptiCompare"));
        
        await Page.GetByRole(AriaRole.Link, new() { NameString = "Phones" }).ClickAsync();
        
        var targetRow = await Page.QuerySelectorAsync(".table > tbody:nth-child(2) > tr:nth-child(3)");
        if (targetRow != null)
        {
            var detailsButton = await targetRow.QuerySelectorAsync("a[href^='/Phones/Details/']");
            await detailsButton.ClickAsync();
            await Expect(Page).ToHaveTitleAsync(new Regex("Phone Details"));
            var targetCellData = await Page.EvalOnSelectorAsync<string>(
                ".tg-wrap > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(2) > td:nth-child(2)",
                "e => e.innerText");

            Assert.That(targetCellData, Is.EqualTo("Google Pixel 7 5G"));
        }
        else
        {
            throw new Exception("Row not found");
        }
    }
}