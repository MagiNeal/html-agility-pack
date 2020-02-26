﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xunit;

namespace HtmlAgilityPack.Tests.NetStandard2_0
{
    public class EncapsulatorTests
    {
        [Fact]
        public void StackOverFlow_Test()
        {
            HtmlWeb stackoverflowSite = new HtmlWeb();
            HtmlDocument htmlDocument = stackoverflowSite.Load("https://stackoverflow.com/questions");

            StackOverflowPage stackOverflowPage = htmlDocument.DocumentNode.GetEncapsulatedData<StackOverflowPage>();

            Assert.NotNull(stackOverflowPage);
        }


    }

    #region StackOverFlow_TestClasses

    [HasXPath]
    public class StackOverflowPage
    {
        
        [XPath("//*[@id='questions']/div")]
        public IEnumerable<StackOverflowQuestion> Questions { get; set; }
        
        
        
        [XPath("//*[@id='hot-network-questions']/ul//li")]
        public IEnumerable<HotNetworkQuestion> GetHotNetworkQuestions { get; set; }
        
    }

    [HasXPath]
    [DebuggerDisplay("StackOverflowQuestion : {Question.QuestionTitle}")]
    public class StackOverflowQuestion
    {

        [XPath("/div[@class='statscontainer']")]
        public StatisticsBox Statistics { get; set; }


        [XPath("/div[@class='summary']")]
        public QuestionBox Question { get; set; }
       
        // The XPath below is an alternativ XPath if the uncommented one does not work
        // [XPath("/div[@class='summary']/div[3]/div")]
        [XPath("/div[@class='summary']/div[@class='started fr']/div")]
        public UserBox User { get; set; }
    }



    [HasXPath]
    [DebuggerDisplay("Votes={Votes} , Answers={Answers} , Views={Views}")]
    public class StatisticsBox
    {
        [XPath("/div[1]/div[1]/div/span/strong")]
        public int Votes { get; set; }

        [XPath("/div[1]/div[2]/strong")]
        public int Answers { get; set; }

        [XPath("/div[2]")]
        public string Views { get; set; }


    }





    [HasXPath]
    [DebuggerDisplay("QuestionTitle={QuestionTitle}")]
    public class QuestionBox
    {
        [XPath("/h3/a")]
        public string QuestionTitle { get; set; }

        [XPath("/h3/a", "href")]
        public string QuestionLink { get; set; }

        [XPath("/div[starts-with(@class,'tags')]//a")]
        public IEnumerable<string> Tags { get; set; }
    }



    [HasXPath]
    [DebuggerDisplay("UserID={UserID} , ReputationScore={ReputationScore}")]
    public class UserBox
    {
        [XPath("/div[@class='user-action-time']/span", "title")]
        public DateTime ExactTime { get; set; }

        [XPath("/div[@class='user-action-time']/span")]
        public string RelativeTime { get; set; }

        [XPath("/div[@class='user-details']/a","href")]
        public string UserLink { get; set; }

        [XPath("/div[@class='user-details']/a")]
        public string UserName { get; set; }

        [XPath("/div[@class='user-details']/div[1]")]
        public string ReputationScore { get; set; }
    }

    [HasXPath]
    [DebuggerDisplay("Question Title={QuestionTitle}")]
    public class HotNetworkQuestion
    {
        [XPath("/div", "title")]
        public string QuestionCategory { get; set; }

        [XPath("/a")]
        public string QuestionTitle { get; set; }

        [XPath("/a", "href")]
        public string QuestionLink { get; set; }
    }

    #endregion #region StackOverFlow_TestClasses

}
