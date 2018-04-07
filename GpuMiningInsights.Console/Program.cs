﻿using CsQuery;
using GpuMiningInsights.Application.Amazon;
using GpuMiningInsights.Core;
using OpenQA.Selenium.PhantomJS;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GpuMiningInsights.Console
{
    class Program
    {
        /*
         * 
         element id for etherum hashrate factor_eth_hr

            issue

             */


        static void Main(string[] args)
        {
            // TestCurrencyApi();
            string searchTerm = "B06Y15M48C,B06Y144RLK,B06ZYC3SW1";// "B071Y7CKM2";
                                                                   // var resultttt2US = AmazonService.SearchItemLookupOperation(searchTerm, Nager.AmazonProductAdvertising.AmazonEndpoint.US);
                                                                   //var resultttt2CA = AmazonService.SearchItemLookupOperation(searchTerm, Nager.AmazonProductAdvertising.AmazonEndpoint.CA);
                                                                   // var resultttt2IN = AmazonService.SearchItemLookupOperation(searchTerm, Nager.AmazonProductAdvertising.AmazonEndpoint.IN);
                                                                   // var resultttt2UK = AmazonService.SearchItemLookupOperation(searchTerm, Nager.AmazonProductAdvertising.AmazonEndpoint.UK);
                                                                   //return;
            bool isTest = false;

            if (isTest)
                Test();
            else
            {
                try
                {
                    var gpus = InsighterService.GetInsights();
                    System.Console.Clear();
                    InsighterService.PushData();
                    System.Console.WriteLine("");
                    ClientGpuListData clientGpuListData = new ClientGpuListData()
                    {
                        Date = DateTime.Now.ToString(Settings.DateFormat),
                        Gpus = gpus
                    };
                    string json = JsonConvert.SerializeObject(clientGpuListData);
                    System.Console.WriteLine("");

                    foreach (var item in gpus)
                    {
                        string gpuName = item.Name;
                        string hashPrice = item.LowestHashPrice?.HashPrice.ToString();
                        string hashPriceSource = item.LowestHashPrice?.Source;
                        //string gpuPriceFromSource = item.PriceSources.FirstOrDefault(s => s.Name == hashPriceSource)?.PriceSourceItems.Min(a => a.Price).ToString();

                        System.Console.WriteLine("************************************************************************");
                        System.Console.WriteLine($"GPU {gpuName} ,ProfitPerYearMinusCostUsd = {item.ProfitPerYearMinusCostUsd}, Revenue ($/Day) = {item.RevenuePerDayUsd}, Profit ($/Day) = {item.ProfitPerDayUsd}  ,HashRate = {item.Hashrate}, HashCost = {hashPrice}, FROM = {hashPriceSource } @ Price ");
                        System.Console.WriteLine("");
                        foreach (var priceSource in item.PriceSources)
                        {

                            System.Console.WriteLine($" priceSource :Name :{priceSource.Name}");
                            System.Console.WriteLine($" priceSource Items:");
                            foreach (var priceSourceItem in priceSource.PriceSourceItems)
                            {
                                System.Console.WriteLine($"item Name :{priceSourceItem.Name},Price :{priceSourceItem.Price},PriceCurrency: {priceSourceItem.PriceCurrency}, mercharnt: {priceSourceItem.Merchant}");

                            }

                        }
                        System.Console.WriteLine("************************************************************************");

                    }
                    string ser = JsonConvert.SerializeObject(gpus);
                    List<GPU> gpustest = JsonConvert.DeserializeObject<List<GPU>>(ser);
                    InsighterService.PushData();
                }
                catch (Exception e)
                {
                    System.Console.WriteLine(e);
                }
                
            }

            //}
            //catch (Exception ex)
            //{
            //    System.Console.WriteLine(ex);

            //}
            System.Console.WriteLine("PAKTC");
            System.Console.ReadLine();

        }

        private static void TestCurrencyApi()
        {
            string baseCurrency = "USD";
            string currencyApiUrl = "http://www.apilayer.net/api/live?access_key=ff139c408a9439cd66d94f7ee26a598b&format=1&source=USD";

            string respomseText = InsighterService.GetHttpResponseText(currencyApiUrl);
            var pricesDictResponse = JsonConvert.DeserializeObject<CurrencyPricesResponse>(respomseText);
            var pricesDict = pricesDictResponse.quotes;
        }

        private static void Test()
        {



            /*
             * 6 columns
             * 1st column the name of the Crypto
             * 7th column for Rev & Profit in $
             * Only one table in the page so thanks
             * <table>
             
             */

            var htmlResponse = "<!DOCTYPE html><html> <head><script type=\"text / javascript\" src=\"https://gc.kis.v2.scr.kaspersky-labs.com/A81ACED4-86EF-894B-A5E6-EF56311C7FB6/main.js\" charset=\"UTF-8\"></script><script type=\"text/javascript\">window.NREUM||(NREUM={});NREUM.info={\"beacon\":\"bam.nr-data.net\",\"errorBeacon\":\"bam.nr-data.net\",\"licenseKey\":\"b25815f649\",\"applicationID\":\"3889467\",\"transactionName\":\"JlcNRxRfX19REU5SXgxWEBwPXldWTA==\",\"queueTime\":1,\"applicationTime\":104,\"agent\":\"\"}</script><script type=\"text/javascript\">window.NREUM||(NREUM={}),__nr_require=function(e,t,n){function r(n){if(!t[n]){var o=t[n]={exports:{}};e[n][0].call(o.exports,function(t){var o=e[n][1][t];return r(o||t)},o,o.exports)}return t[n].exports}if(\"function\"==typeof __nr_require)return __nr_require;for(var o=0;o<n.length;o++)r(n[o]);return r}({1:[function(e,t,n){function r(){}function o(e,t,n){return function(){return i(e,[f.now()].concat(u(arguments)),t?null:this,n),t?void 0:this}}var i=e(\"handle\"),a=e(2),u=e(3),c=e(\"ee\").get(\"tracer\"),f=e(\"loader\"),s=NREUM;\"undefined\"==typeof window.newrelic&&(newrelic=s);var p=[\"setPageViewName\",\"setCustomAttribute\",\"setErrorHandler\",\"finished\",\"addToTrace\",\"inlineHit\",\"addRelease\"],d=\"api-\",l=d+\"ixn-\";a(p,function(e,t){s[t]=o(d+t,!0,\"api\")}),s.addPageAction=o(d+\"addPageAction\",!0),s.setCurrentRouteName=o(d+\"routeName\",!0),t.exports=newrelic,s.interaction=function(){return(new r).get()};var m=r.prototype={createTracer:function(e,t){var n={},r=this,o=\"function\"==typeof t;return i(l+\"tracer\",[f.now(),e,n],r),function(){if(c.emit((o?\"\":\"no-\")+\"fn-start\",[f.now(),r,o],n),o)try{return t.apply(this,arguments)}catch(e){throw c.emit(\"fn-err\",[arguments,this,e],n),e}finally{c.emit(\"fn-end\",[f.now()],n)}}}};a(\"setName,setAttribute,save,ignore,onEnd,getContext,end,get\".split(\",\"),function(e,t){m[t]=o(l+t)}),newrelic.noticeError=function(e){\"string\"==typeof e&&(e=new Error(e)),i(\"err\",[e,f.now()])}},{}],2:[function(e,t,n){function r(e,t){var n=[],r=\"\",i=0;for(r in e)o.call(e,r)&&(n[i]=t(r,e[r]),i+=1);return n}var o=Object.prototype.hasOwnProperty;t.exports=r},{}],3:[function(e,t,n){function r(e,t,n){t||(t=0),\"undefined\"==typeof n&&(n=e?e.length:0);for(var r=-1,o=n-t||0,i=Array(o<0?0:o);++r<o;)i[r]=e[t+r];return i}t.exports=r},{}],4:[function(e,t,n){t.exports={exists:\"undefined\"!=typeof window.performance&&window.performance.timing&&\"undefined\"!=typeof window.performance.timing.navigationStart}},{}],ee:[function(e,t,n){function r(){}function o(e){function t(e){return e&&e instanceof r?e:e?c(e,u,i):i()}function n(n,r,o,i){if(!d.aborted||i){e&&e(n,r,o);for(var a=t(o),u=m(n),c=u.length,f=0;f<c;f++)u[f].apply(a,r);var p=s[y[n]];return p&&p.push([b,n,r,a]),a}}function l(e,t){v[e]=m(e).concat(t)}function m(e){return v[e]||[]}function w(e){return p[e]=p[e]||o(n)}function g(e,t){f(e,function(e,n){t=t||\"feature\",y[n]=t,t in s||(s[t]=[])})}var v={},y={},b={on:l,emit:n,get:w,listeners:m,context:t,buffer:g,abort:a,aborted:!1};return b}function i(){return new r}function a(){(s.api||s.feature)&&(d.aborted=!0,s=d.backlog={})}var u=\"nr@context\",c=e(\"gos\"),f=e(2),s={},p={},d=t.exports=o();d.backlog=s},{}],gos:[function(e,t,n){function r(e,t,n){if(o.call(e,t))return e[t];var r=n();if(Object.defineProperty&&Object.keys)try{return Object.defineProperty(e,t,{value:r,writable:!0,enumerable:!1}),r}catch(i){}return e[t]=r,r}var o=Object.prototype.hasOwnProperty;t.exports=r},{}],handle:[function(e,t,n){function r(e,t,n,r){o.buffer([e],r),o.emit(e,t,n)}var o=e(\"ee\").get(\"handle\");t.exports=r,r.ee=o},{}],id:[function(e,t,n){function r(e){var t=typeof e;return!e||\"object\"!==t&&\"function\"!==t?-1:e===window?0:a(e,i,function(){return o++})}var o=1,i=\"nr@id\",a=e(\"gos\");t.exports=r},{}],loader:[function(e,t,n){function r(){if(!x++){var e=h.info=NREUM.info,t=d.getElementsByTagName(\"script\")[0];if(setTimeout(s.abort,3e4),!(e&&e.licenseKey&&e.applicationID&&t))return s.abort();f(y,function(t,n){e[t]||(e[t]=n)}),c(\"mark\",[\"onload\",a()+h.offset],null,\"api\");var n=d.createElement(\"script\");n.src=\"https://\"+e.agent,t.parentNode.insertBefore(n,t)}}function o(){\"complete\"===d.readyState&&i()}function i(){c(\"mark\",[\"domContent\",a()+h.offset],null,\"api\")}function a(){return E.exists&&performance.now?Math.round(performance.now()):(u=Math.max((new Date).getTime(),u))-h.offset}var u=(new Date).getTime(),c=e(\"handle\"),f=e(2),s=e(\"ee\"),p=window,d=p.document,l=\"addEventListener\",m=\"attachEvent\",w=p.XMLHttpRequest,g=w&&w.prototype;NREUM.o={ST:setTimeout,SI:p.setImmediate,CT:clearTimeout,XHR:w,REQ:p.Request,EV:p.Event,PR:p.Promise,MO:p.MutationObserver};var v=\"\"+location,y={beacon:\"bam.nr-data.net\",errorBeacon:\"bam.nr-data.net\",agent:\"js-agent.newrelic.com/nr-1071.min.js\"},b=w&&g&&g[l]&&!/CriOS/.test(navigator.userAgent),h=t.exports={offset:u,now:a,origin:v,features:{},xhrWrappable:b};e(1),d[l]?(d[l](\"DOMContentLoaded\",i,!1),p[l](\"load\",r,!1)):(d[m](\"onreadystatechange\",o),p[m](\"onload\",r)),c(\"mark\",[\"firstbyte\",u],null,\"api\");var x=0,E=e(4)},{}]},{},[\"loader\"]);</script> <title>WhatToMine - Crypto coins mining profit calculator compared to Ethereum </title> <meta name=\"description\" content=\"Using WhatToMine you can check, how profitable it is to mine selected altcoins in comparison to ethereum or bitcoin\"> <meta name=\"keywords\" content=\"Mining Calculator Bitcoin Ethereum Zcash X11 Blake\"> <link rel=\"stylesheet\" media=\"all\" href=\"/assets/application-fba3d0f60d35607aedb174ea4ca9ba63ac319ff56e5278e1a641d9a1298ef42a.css\"/> <script src=\"/assets/application-04dfa8693dd5546bf72d0a60c572d536572331afb0008847577ec37663e4e34c.js\"></script> <meta name=\"csrf-param\" content=\"authenticity_token\"/><meta name=\"csrf-token\" content=\"GWj+kMxpBc1IhWmUN/p7i7gPHhJhWefNuarliOWdAWmfa2L6zvJTUTN9y7+ixrIlvhJO9POFPmhTVAVn9pcUGA==\"/> <script>(function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){(i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o), m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)})(window,document,'script','https://www.google-analytics.com/analytics.js','ga'); ga('create', 'UA-49963845-1', 'auto'); ga('send', 'pageview'); </script> </head> <body class=lower> <header class=\"navbar navbar-fixed-top navbar-inverse\"> <div class=\"container\"> <div class=\"tickers\"> <ul class=\"nav navbar-nav\"> <li> <img width=\"20px\" class=\"ticker-image\" title=\"Bitcoin\" src=\"https://images.whattomine.com/coins/logos/000/000/001/thumb/btclogo.png?1486175435\" alt=\"Btclogo\"/> $9,724.70 </li><li> <img width=\"20px\" class=\"ticker-image\" title=\"Litecoin\" src=\"https://images.whattomine.com/coins/logos/000/000/004/thumb/litecoin-logo.png?1486175484\" alt=\"Litecoin logo\"/> $214.73 </li><li> <img width=\"20px\" class=\"ticker-image\" title=\"Dash\" src=\"https://images.whattomine.com/coins/logos/000/000/034/thumb/dash-darkcoin.png?1486175489\" alt=\"Dash darkcoin\"/> $592.72 </li><li> <img width=\"20px\" class=\"ticker-image\" title=\"Monero\" src=\"https://images.whattomine.com/coins/logos/000/000/101/thumb/monero.png?1486175477\" alt=\"Monero\"/> $270.83 </li><li> <img width=\"20px\" class=\"ticker-image\" title=\"Ethereum\" src=\"https://images.whattomine.com/coins/logos/000/000/151/thumb/eth.png?1486214743\" alt=\"Eth\"/> $843.82 </li><li> <img width=\"20px\" class=\"ticker-image\" title=\"Zcash\" src=\"https://images.whattomine.com/coins/logos/000/000/166/thumb/zec.png?1486175433\" alt=\"Zec\"/> $395.47 </li><li> <img width=\"20px\" class=\"ticker-image\" title=\"EthereumClassic\" src=\"https://images.whattomine.com/coins/logos/000/000/162/thumb/etc3.png?1496094431\" alt=\"Etc3\"/> $35.78 </li></ul> </div><div style=\"float: right; margin-top: 10px; margin-bottom: -10px\"> <a href=\"https://twitter.com/WhatToMine\" class=\"twitter-follow-button\" data-show-count=\"true\"> Follow @WhatToMine </a> <script>window.twttr=(function(d, s, id){var js, fjs=d.getElementsByTagName(s)[0], t=window.twttr ||{}; if (d.getElementById(id)) return t; js=d.createElement(s); js.id=id; js.src=\"https://platform.twitter.com/widgets.js\"; fjs.parentNode.insertBefore(js, fjs); t._e=[]; t.ready=function(f){t._e.push(f);}; return t;}(document, \"script\", \"twitter-wjs\"));</script> </div></div><nav> <div class=\"container\"> <div class=\"navbar-header\"> <a id=\"logo\" class=\"navbar-brand\" href=\"/coins\">What To Mine</a> </div><ul class=\"nav nav-tabs pull-left stick-bottom primary\"> <li class=\"active\"><a href=\"/coins\">GPU</a></li><li class=\"\"><a href=\"/asic\">ASIC</a></li><li class=\"\"> <a href=\"/calculators\">Coins</a> <h6 class=\"label-new\"> <span class=\"label label-danger\">New</span> </h6> </li><li class=\"\"> <a href=\"#\" class=\"dropdown-toggle\" data-toggle=\"dropdown\"> ETH+ <span class=\"caret\"></span> </a> <ul class=\"dropdown-menu\" role=\"menu\"> <li><a href=\"/merged_coins/1-eth-dcr\">ETH+DCR</a></li><li><a href=\"/merged_coins/2-eth-sc\">ETH+SC</a></li><li><a href=\"/merged_coins/7-eth-lbc\">ETH+LBC</a></li><li><a href=\"/merged_coins/10-eth-pasc\">ETH+PASC</a></li><li><a href=\"/merged_coins/13-eth-pasl\">ETH+PASL</a></li><li><a href=\"/merged_coins/41-eth-max\">ETH+MAX</a></li><li><a href=\"/merged_coins/42-eth-smart\">ETH+SMART</a></li><li><a href=\"/merged_coins/43-eth-xvg\">ETH+XVG</a></li></ul> </li><li class=\"\"> <a href=\"#\" class=\"dropdown-toggle\" data-toggle=\"dropdown\"> ETC+ <span class=\"caret\"></span> </a> <ul class=\"dropdown-menu\" role=\"menu\"> <li><a href=\"/merged_coins/3-etc-dcr\">ETC+DCR</a></li><li><a href=\"/merged_coins/4-etc-sc\">ETC+SC</a></li><li><a href=\"/merged_coins/8-etc-lbc\">ETC+LBC</a></li><li><a href=\"/merged_coins/11-etc-pasc\">ETC+PASC</a></li><li><a href=\"/merged_coins/14-etc-pasl\">ETC+PASL</a></li><li><a href=\"/merged_coins/62-etc-max\">ETC+MAX</a></li><li><a href=\"/merged_coins/63-etc-smart\">ETC+SMART</a></li><li><a href=\"/merged_coins/64-etc-xvg\">ETC+XVG</a></li></ul> </li><li class=\"\"> <a href=\"#\" class=\"dropdown-toggle\" data-toggle=\"dropdown\"> EXP+ <span class=\"caret\"></span> </a> <ul class=\"dropdown-menu\" role=\"menu\"> <li><a href=\"/merged_coins/5-exp-dcr\">EXP+DCR</a></li><li><a href=\"/merged_coins/6-exp-sc\">EXP+SC</a></li><li><a href=\"/merged_coins/9-exp-lbc\">EXP+LBC</a></li><li><a href=\"/merged_coins/12-exp-pasc\">EXP+PASC</a></li><li><a href=\"/merged_coins/15-exp-pasl\">EXP+PASL</a></li><li><a href=\"/merged_coins/47-exp-max\">EXP+MAX</a></li><li><a href=\"/merged_coins/48-exp-smart\">EXP+SMART</a></li><li><a href=\"/merged_coins/49-exp-xvg\">EXP+XVG</a></li></ul> </li></ul> <ul class=\"nav nav-tabs pull-right stick-bottom primary\"> <li> </li><li><a href=\"/coins.json\">JSON</a></li><li class=\"\"> <a href=\"/contacts/new\">Contact</a> </li></ul> </div><div class=\"navbar-default\"> <div class=\"container\"> <ul class=\"nav nav-tabs secondary navbar-left\"> <li class=\"active\"> <a href=\"/coins?dataset=Main\">Main</a> <div class=\"label-remove\"> <a class=\"label label-danger\" data-confirm=\"You are about to remove Main dataset\" rel=\"nofollow\" data-method=\"delete\" href=\"/remove_dataset?dataset=Main&amp;prefix=index\">X</a> </div></li></ul> <form class=\"navbar-form navbar-right add-dataset\" action=\"/create_dataset?prefix=index\" accept-charset=\"UTF-8\" method=\"post\"><input name=\"utf8\" type=\"hidden\" value=\"&#x2713;\"/><input type=\"hidden\" name=\"authenticity_token\" value=\"ZzDGxNKlu4qxFHl8f0uZ/taSp2d8JwFP3w27DbabUQ/maTd6aFwu8ZV/BI7CCDxnFPdKBW81jQtBsKiCWVUp1w==\"/> <div class=\"input-group\"> <input type=\"text\" name=\"dataset\" id=\"dataset\" value=\"\" class=\"form-control\" placeholder=\"Add new dataset\"/> <span class=\"input-group-btn\"> <input type=\"submit\" name=\"commit\" value=\"Add\" class=\"btn btn-default\" data-disable-with=\"Add\"/> </span> </div></form> </div></div></nav></header> <div class=\"container\"> <script async src='//appsha1.cointraffic.io/js/?wkey=YgxBHL'></script><script async src=\"//pagead2.googlesyndication.com/pagead/js/adsbygoogle.js\"></script> <script>(adsbygoogle=window.adsbygoogle || []).push({google_ad_client: \"ca-pub-2477602342455493\", enable_page_level_ads: true}); </script><script data-cfasync='false' type='text/javascript'> window.apd_options={'websiteId': 7052, 'runFromFrame': false}; (function(){var w=window.apd_options.runFromFrame ? window.top : window; if(window.apd_options.runFromFrame && w!=window.parent) w=window.parent; if (w.location.hash.indexOf('apdAdmin') !=-1){if(typeof(Storage) !=='undefined'){w.localStorage.apdAdmin=1;}}var adminMode=((typeof(Storage)=='undefined') || (w.localStorage.apdAdmin==1)); w.apd_options=window.apd_options; var apd=w.document.createElement('script'); apd.type='text/javascript'; apd.async=true; apd.src='//' + (adminMode ? 'cdn' : 'ecdn') + '.firstimpression.io/' + (adminMode ? 'fi.js?id=' + window.apd_options.websiteId : 'fi_client.js') ; var s=w.document.getElementsByTagName('head')[0]; s.appendChild(apd);})(); </script><div class=\"margin-bot-10\"> <div class=\"centered-image\"> <ins class=\"adsbygoogle\" style=\"display:inline-block;width:728px;height:90px\" data-ad-client=\"ca-pub-2477602342455493\" data-ad-slot=\"6419161569\"></ins> <script>(adsbygoogle=window.adsbygoogle || []).push({});</script></div></div><form class=\"new_factor\" id=\"new_factor\" action=\"/coins\" accept-charset=\"UTF-8\" method=\"get\"><input name=\"utf8\" type=\"hidden\" value=\"&#x2713;\"/> <div class=\"row\"> <div class=\"col-xs-3\"> <div class=\"row\"> <div class=\"col-xs-6\"> <div class=\"input-group\"> <input type=\"text\" name=\"adapt_q_280x\" id=\"adapt_q_280x\" value=\"0\" class=\"form-control input-sm adapt-quantity\"/> <div class=\"input-group-btn hash-adapt ck-button-amd\"> <label> <input type=\"checkbox\" name=\"adapt_280x\" id=\"adapt_280x\" value=\"true\" hidden=\"hidden\" class=\"adapt\"/> <span class=\"btn btn-default btn-sm popover-as-title\" data-content=\"Insert hash rates and powers for a number of 280x<br><br>Using 1100/1500 with 0.1V undervolt\" data-placement=\"bottom\" data-trigger=\"hover\" data-html=\"true\"> 280x </span> </label> </div></div></div><div class=\"col-xs-6\"> <div class=\"input-group\"> <input type=\"text\" name=\"adapt_q_380\" id=\"adapt_q_380\" value=\"0\" class=\"form-control input-sm adapt-quantity\"/> <div class=\"input-group-btn hash-adapt ck-button-amd\"> <label> <input type=\"checkbox\" name=\"adapt_380\" id=\"adapt_380\" value=\"true\" hidden=\"hidden\" class=\"adapt\"/> <span class=\"btn btn-default btn-sm popover-as-title\" data-content=\"Insert hash rates and powers for a number of 380<br><br>Using 1000/1500 with 0.1V undervolt\" data-placement=\"bottom\" data-trigger=\"hover\" data-html=\"true\"> 380 </span> </label> </div></div></div></div></div><div class=\"col-xs-3\"> <div class=\"row\"> <div class=\"col-xs-6\"> <div class=\"input-group\"> <input type=\"text\" name=\"adapt_q_fury\" id=\"adapt_q_fury\" value=\"0\" class=\"form-control input-sm adapt-quantity\"/> <div class=\"input-group-btn hash-adapt ck-button-amd\"> <label> <input type=\"checkbox\" name=\"adapt_fury\" id=\"adapt_fury\" value=\"true\" hidden=\"hidden\" class=\"adapt\"/> <span class=\"btn btn-default btn-sm popover-as-title\" data-content=\"Insert hash rates and powers for a number of Fury<br><br>Using 1020/500 with 0.1V undervolt\" data-placement=\"bottom\" data-trigger=\"hover\" data-html=\"true\"> Fury </span> </label> </div></div></div><div class=\"col-xs-6\"> <div class=\"input-group\"> <input type=\"text\" name=\"adapt_q_470\" id=\"adapt_q_470\" value=\"0\" class=\"form-control input-sm adapt-quantity\"/> <div class=\"input-group-btn hash-adapt ck-button-amd\"> <label> <input type=\"checkbox\" name=\"adapt_470\" id=\"adapt_470\" value=\"true\" hidden=\"hidden\" class=\"adapt\"/> <span class=\"btn btn-default btn-sm popover-as-title\" data-content=\"Insert hash rates and powers for a number of 470 4GB<br><br>Using 1050/1870 with 0.2V undervolt and ETH bios mod\" data-placement=\"bottom\" data-trigger=\"hover\" data-html=\"true\"> 470 </span> </label> </div></div></div></div></div><div class=\"col-xs-3\"> <div class=\"row\"> <div class=\"col-xs-6\"> <div class=\"input-group\"> <input type=\"text\" name=\"adapt_q_480\" id=\"adapt_q_480\" value=\"3\" class=\"form-control input-sm adapt-quantity\"/> <div class=\"input-group-btn hash-adapt ck-button-amd\"> <label> <input type=\"checkbox\" name=\"adapt_480\" id=\"adapt_480\" value=\"true\" hidden=\"hidden\" class=\"adapt\" checked=\"checked\"/> <span class=\"btn btn-default btn-sm popover-as-title\" data-content=\"Insert hash rates and powers for a number of 480 8GB<br><br>Using 1150/2150 with 0.2V undervolt and ETH bios mod\" data-placement=\"bottom\" data-trigger=\"hover\" data-html=\"true\"> 480 </span> </label> </div></div></div><div class=\"col-xs-6\"> <div class=\"input-group\"> <input type=\"text\" name=\"adapt_q_570\" id=\"adapt_q_570\" value=\"0\" class=\"form-control input-sm adapt-quantity\"/> <div class=\"input-group-btn hash-adapt ck-button-amd\"> <label> <input type=\"checkbox\" name=\"adapt_570\" id=\"adapt_570\" value=\"true\" hidden=\"hidden\" class=\"adapt\"/> <span class=\"btn btn-default btn-sm popover-as-title\" data-content=\"Insert hash rates and powers for a number of 570 4GB<br><br>Using 1100/2000 with 0.2V undervolt and ETH bios mod\" data-placement=\"bottom\" data-trigger=\"hover\" data-html=\"true\"> 570 </span> </label> </div></div></div></div></div><div class=\"col-xs-3\"> <div class=\"row\"> <div class=\"col-xs-6\"> <div class=\"input-group\"> <input type=\"text\" name=\"adapt_q_580\" id=\"adapt_q_580\" value=\"0\" class=\"form-control input-sm adapt-quantity\"/> <div class=\"input-group-btn hash-adapt ck-button-amd\"> <label> <input type=\"checkbox\" name=\"adapt_580\" id=\"adapt_580\" value=\"true\" hidden=\"hidden\" class=\"adapt\"/> <span class=\"btn btn-default btn-sm popover-as-title\" data-content=\"Insert hash rates and powers for a number of 580 8GB<br><br>Using 1150/2150 with 0.2V undervolt and ETH bios mod\" data-placement=\"bottom\" data-trigger=\"hover\" data-html=\"true\"> 580 </span> </label> </div></div></div><div class=\"col-xs-6\"> <div class=\"input-group\"> <input type=\"text\" name=\"adapt_q_vega56\" id=\"adapt_q_vega56\" value=\"0\" class=\"form-control input-sm adapt-quantity\"/> <div class=\"input-group-btn hash-adapt ck-button-amd\"> <label> <input type=\"checkbox\" name=\"adapt_vega56\" id=\"adapt_vega56\" value=\"true\" hidden=\"hidden\" class=\"adapt\"/> <span class=\"btn btn-default btn-sm popover-as-title\" data-content=\"Insert hash rates and powers for a number of Vega 56<br><br>Using 950/900 with 0.95V and blockchain driver\" data-placement=\"bottom\" data-trigger=\"hover\" data-html=\"true\"> Vega56 </span> </label> </div></div></div></div></div><div class=\"col-xs-3\"> <div class=\"row\"> <div class=\"col-xs-6\"> <div class=\"input-group\"> <input type=\"text\" name=\"adapt_q_vega64\" id=\"adapt_q_vega64\" value=\"0\" class=\"form-control input-sm adapt-quantity\"/> <div class=\"input-group-btn hash-adapt ck-button-amd\"> <label> <input type=\"checkbox\" name=\"adapt_vega64\" id=\"adapt_vega64\" value=\"true\" hidden=\"hidden\" class=\"adapt\"/> <span class=\"btn btn-default btn-sm popover-as-title\" data-content=\"Insert hash rates and powers for a number of Vega 64<br><br>Using 950/1020 with 0.975V and blockchain driver\" data-placement=\"bottom\" data-trigger=\"hover\" data-html=\"true\"> Vega64 </span> </label> </div></div></div><div class=\"col-xs-6\"> <div class=\"input-group\"> <input type=\"text\" name=\"adapt_q_750Ti\" id=\"adapt_q_750Ti\" value=\"0\" class=\"form-control input-sm adapt-quantity\"/> <div class=\"input-group-btn hash-adapt ck-button-nv\"> <label> <input type=\"checkbox\" name=\"adapt_750Ti\" id=\"adapt_750Ti\" value=\"true\" hidden=\"hidden\" class=\"adapt\"/> <span class=\"btn btn-default btn-sm popover-as-title\" data-content=\"Insert hash rates and powers for a number of 750Ti<br><br>Using 1350/1500 with 100% TDP\" data-placement=\"bottom\" data-trigger=\"hover\" data-html=\"true\"> 750Ti </span> </label> </div></div></div></div></div><div class=\"col-xs-3\"> <div class=\"row\"> <div class=\"col-xs-6\"> <div class=\"input-group\"> <input type=\"text\" name=\"adapt_q_1050Ti\" id=\"adapt_q_1050Ti\" value=\"0\" class=\"form-control input-sm adapt-quantity\"/> <div class=\"input-group-btn hash-adapt ck-button-nv\"> <label> <input type=\"checkbox\" name=\"adapt_1050Ti\" id=\"adapt_1050Ti\" value=\"true\" hidden=\"hidden\" class=\"adapt\"/> <span class=\"btn btn-default btn-sm popover-as-title\" data-content=\"Insert hash rates and powers for a number of 1050Ti<br><br>Using +150/+500 with 75% TDP\" data-placement=\"bottom\" data-trigger=\"hover\" data-html=\"true\"> 1050Ti </span> </label> </div></div></div><div class=\"col-xs-6\"> <div class=\"input-group\"> <input type=\"text\" name=\"adapt_q_10606\" id=\"adapt_q_10606\" value=\"0\" class=\"form-control input-sm adapt-quantity\"/> <div class=\"input-group-btn hash-adapt ck-button-nv\"> <label> <input type=\"checkbox\" name=\"adapt_10606\" id=\"adapt_10606\" value=\"true\" hidden=\"hidden\" class=\"adapt\"/> <span class=\"btn btn-default btn-sm popover-as-title\" data-content=\"Insert hash rates and powers for a number of 1060 6GB<br><br>Using +150/+500 with 65% TDP\" data-placement=\"bottom\" data-trigger=\"hover\" data-html=\"true\"> 1060 </span> </label> </div></div></div></div></div><div class=\"col-xs-3\"> <div class=\"row\"> <div class=\"col-xs-6\"> <div class=\"input-group\"> <input type=\"text\" name=\"adapt_q_1070\" id=\"adapt_q_1070\" value=\"0\" class=\"form-control input-sm adapt-quantity\"/> <div class=\"input-group-btn hash-adapt ck-button-nv\"> <label> <input type=\"checkbox\" name=\"adapt_1070\" id=\"adapt_1070\" value=\"true\" hidden=\"hidden\" class=\"adapt\"/> <span class=\"btn btn-default btn-sm popover-as-title\" data-content=\"Insert hash rates and powers for a number of 1070<br><br>Using +150/+500 with 65% TDP\" data-placement=\"bottom\" data-trigger=\"hover\" data-html=\"true\"> 1070 </span> </label> </div></div></div><div class=\"col-xs-6\"> <div class=\"input-group\"> <input type=\"text\" name=\"adapt_q_1070Ti\" id=\"adapt_q_1070Ti\" value=\"0\" class=\"form-control input-sm adapt-quantity\"/> <div class=\"input-group-btn hash-adapt ck-button-nv\"> <label> <input type=\"checkbox\" name=\"adapt_1070Ti\" id=\"adapt_1070Ti\" value=\"true\" hidden=\"hidden\" class=\"adapt\"/> <span class=\"btn btn-default btn-sm popover-as-title\" data-content=\"Insert hash rates and powers for a number of 1070Ti<br><br>Using +150/+500 with 65% TDP\" data-placement=\"bottom\" data-trigger=\"hover\" data-html=\"true\"> 1070Ti </span> </label> </div></div></div></div></div><div class=\"col-xs-3\"> <div class=\"row\"> <div class=\"col-xs-6\"> <div class=\"input-group\"> <input type=\"text\" name=\"adapt_q_1080\" id=\"adapt_q_1080\" value=\"0\" class=\"form-control input-sm adapt-quantity\"/> <div class=\"input-group-btn hash-adapt ck-button-nv\"> <label> <input type=\"checkbox\" name=\"adapt_1080\" id=\"adapt_1080\" value=\"true\" hidden=\"hidden\" class=\"adapt\"/> <span class=\"btn btn-default btn-sm popover-as-title\" data-content=\"Insert hash rates and powers for a number of 1080<br><br>Using +150/+500 with 65% TDP\" data-placement=\"bottom\" data-trigger=\"hover\" data-html=\"true\"> 1080 </span> </label> </div></div></div><div class=\"col-xs-6\"> <div class=\"input-group\"> <input type=\"text\" name=\"adapt_q_1080Ti\" id=\"adapt_q_1080Ti\" value=\"0\" class=\"form-control input-sm adapt-quantity\"/> <div class=\"input-group-btn hash-adapt ck-button-nv\"> <label> <input type=\"checkbox\" name=\"adapt_1080Ti\" id=\"adapt_1080Ti\" value=\"true\" hidden=\"hidden\" class=\"adapt\"/> <span class=\"btn btn-default btn-sm popover-as-title\" data-content=\"Insert hash rates and powers for a number of 1080Ti<br><br>Using +150/+500 with 65% TDP\" data-placement=\"bottom\" data-trigger=\"hover\" data-html=\"true\"> 1080Ti </span> </label> </div></div></div></div></div></div><div class=\"row\" style=\"margin-bottom: 10px\"> <div class=\"col-xs-3\"> <div class=\"row\"> <div class=\"col-xs-6\"> <div class=\"ck-button\"> <label> <input type=\"checkbox\" name=\"eth\" id=\"eth\" value=\"true\" hidden=\"hidden\" checked=\"checked\"/> <span class=\"btn btn-default popover-as-title\" data-content=\"Include Ethash\" data-placement=\"bottom\" data-trigger=\"hover\"> Ethash </span> </label></div><label class=\"small_form\" for=\"hash_rate\">Hash rate</label><div class=\"input-group\"> <input class=\"form-control input-sm\" type=\"text\" value=\"84.0\" name=\"factor[eth_hr]\" id=\"factor_eth_hr\"/> <div class=\"input-group-addon\"> Mh/s </div></div><label class=\"small_form\" for=\"power\">Power</label><div class=\"input-group\"> <input class=\"form-control input-sm\" type=\"text\" value=\"405.0\" name=\"factor[eth_p]\" id=\"factor_eth_p\"/> <div class=\"input-group-addon\"> W </div></div><label class=\"small_form\" for=\"daily_cost\">Daily cost</label><div class=\"input\"> <input type=\"text\" name=\"Ethash daily cost\" id=\"Ethash_daily_cost\" value=\"$0.97\" disabled=\"disabled\" class=\"form-control input-sm\"/></div></div><div class=\"col-xs-6\"> <div class=\"ck-button\"> <label> <input type=\"checkbox\" name=\"grof\" id=\"grof\" value=\"true\" hidden=\"hidden\"/> <span class=\"btn btn-default popover-as-title\" data-content=\"Include Groestl\" data-placement=\"bottom\" data-trigger=\"hover\"> Groestl </span> </label></div><label class=\"small_form\" for=\"hash_rate\">Hash rate</label><div class=\"input-group\"> <input class=\"form-control input-sm\" type=\"text\" value=\"63.9\" name=\"factor[gro_hr]\" id=\"factor_gro_hr\"/> <div class=\"input-group-addon\"> Mh/s </div></div><label class=\"small_form\" for=\"power\">Power</label><div class=\"input-group\"> <input class=\"form-control input-sm\" type=\"text\" value=\"450.0\" name=\"factor[gro_p]\" id=\"factor_gro_p\"/> <div class=\"input-group-addon\"> W </div></div><label class=\"small_form\" for=\"daily_cost\">Daily cost</label><div class=\"input\"> <input type=\"text\" name=\"Groestl daily cost\" id=\"Groestl_daily_cost\" value=\"$1.08\" disabled=\"disabled\" class=\"form-control input-sm\"/></div></div></div></div><div class=\"col-xs-3\"> <div class=\"row\"> <div class=\"col-xs-6\"> <div class=\"ck-button\"> <label> <input type=\"checkbox\" name=\"x11gf\" id=\"x11gf\" value=\"true\" hidden=\"hidden\"/> <span class=\"btn btn-default popover-as-title\" data-content=\"Include X11Gost\" data-placement=\"bottom\" data-trigger=\"hover\"> X11Gost </span> </label></div><label class=\"small_form\" for=\"hash_rate\">Hash rate</label><div class=\"input-group\"> <input class=\"form-control input-sm\" type=\"text\" value=\"20.1\" name=\"factor[x11g_hr]\" id=\"factor_x11g_hr\"/> <div class=\"input-group-addon\"> Mh/s </div></div><label class=\"small_form\" for=\"power\">Power</label><div class=\"input-group\"> <input class=\"form-control input-sm\" type=\"text\" value=\"420.0\" name=\"factor[x11g_p]\" id=\"factor_x11g_p\"/> <div class=\"input-group-addon\"> W </div></div><label class=\"small_form\" for=\"daily_cost\">Daily cost</label><div class=\"input\"> <input type=\"text\" name=\"X11Gost daily cost\" id=\"X11Gost_daily_cost\" value=\"$1.01\" disabled=\"disabled\" class=\"form-control input-sm\"/></div></div><div class=\"col-xs-6\"> <div class=\"ck-button\"> <label> <input type=\"checkbox\" name=\"cn\" id=\"cn\" value=\"true\" hidden=\"hidden\"/> <span class=\"btn btn-default popover-as-title\" data-content=\"Include CryptoNight\" data-placement=\"bottom\" data-trigger=\"hover\"> CryptoNight </span> </label></div><label class=\"small_form\" for=\"hash_rate\">Hash rate</label><div class=\"input-group\"> <input class=\"form-control input-sm\" type=\"text\" value=\"2190.0\" name=\"factor[cn_hr]\" id=\"factor_cn_hr\"/> <div class=\"input-group-addon\"> h/s </div></div><label class=\"small_form\" for=\"power\">Power</label><div class=\"input-group\"> <input class=\"form-control input-sm\" type=\"text\" value=\"330.0\" name=\"factor[cn_p]\" id=\"factor_cn_p\"/> <div class=\"input-group-addon\"> W </div></div><label class=\"small_form\" for=\"daily_cost\">Daily cost</label><div class=\"input\"> <input type=\"text\" name=\"CryptoNight daily cost\" id=\"CryptoNight_daily_cost\" value=\"$0.79\" disabled=\"disabled\" class=\"form-control input-sm\"/></div></div></div></div><div class=\"col-xs-3\"> <div class=\"row\"> <div class=\"col-xs-6\"> <div class=\"ck-button\"> <label> <input type=\"checkbox\" name=\"eq\" id=\"eq\" value=\"true\" hidden=\"hidden\"/> <span class=\"btn btn-default popover-as-title\" data-content=\"Include Equihash\" data-placement=\"bottom\" data-trigger=\"hover\"> Equihash </span> </label></div><label class=\"small_form\" for=\"hash_rate\">Hash rate</label><div class=\"input-group\"> <input class=\"form-control input-sm\" type=\"text\" value=\"870.0\" name=\"factor[eq_hr]\" id=\"factor_eq_hr\"/> <div class=\"input-group-addon\"> h/s </div></div><label class=\"small_form\" for=\"power\">Power</label><div class=\"input-group\"> <input class=\"form-control input-sm\" type=\"text\" value=\"360.0\" name=\"factor[eq_p]\" id=\"factor_eq_p\"/> <div class=\"input-group-addon\"> W </div></div><label class=\"small_form\" for=\"daily_cost\">Daily cost</label><div class=\"input\"> <input type=\"text\" name=\"Equihash daily cost\" id=\"Equihash_daily_cost\" value=\"$0.86\" disabled=\"disabled\" class=\"form-control input-sm\"/></div></div><div class=\"col-xs-6\"> <div class=\"ck-button\"> <label> <input type=\"checkbox\" name=\"lre\" id=\"lre\" value=\"true\" hidden=\"hidden\"/> <span class=\"btn btn-default popover-as-title\" data-content=\"Include Lyra2REv2\" data-placement=\"bottom\" data-trigger=\"hover\"> Lyra2REv2 </span> </label></div><label class=\"small_form\" for=\"hash_rate\">Hash rate</label><div class=\"input-group\"> <input class=\"form-control input-sm\" type=\"text\" value=\"14700.0\" name=\"factor[lrev2_hr]\" id=\"factor_lrev2_hr\"/> <div class=\"input-group-addon\"> kh/s </div></div><label class=\"small_form\" for=\"power\">Power</label><div class=\"input-group\"> <input class=\"form-control input-sm\" type=\"text\" value=\"390.0\" name=\"factor[lrev2_p]\" id=\"factor_lrev2_p\"/> <div class=\"input-group-addon\"> W </div></div><label class=\"small_form\" for=\"daily_cost\">Daily cost</label><div class=\"input\"> <input type=\"text\" name=\"Lyra2REv2 daily cost\" id=\"Lyra2REv2_daily_cost\" value=\"$0.94\" disabled=\"disabled\" class=\"form-control input-sm\"/></div></div></div></div><div class=\"col-xs-3\"> <div class=\"row\"> <div class=\"col-xs-6\"> <div class=\"ck-button\"> <label> <input type=\"checkbox\" name=\"ns\" id=\"ns\" value=\"true\" hidden=\"hidden\"/> <span class=\"btn btn-default popover-as-title\" data-content=\"Include NeoScrypt\" data-placement=\"bottom\" data-trigger=\"hover\"> NeoScrypt </span> </label></div><label class=\"small_form\" for=\"hash_rate\">Hash rate</label><div class=\"input-group\"> <input class=\"form-control input-sm\" type=\"text\" value=\"1950.0\" name=\"factor[ns_hr]\" id=\"factor_ns_hr\"/> <div class=\"input-group-addon\"> kh/s </div></div><label class=\"small_form\" for=\"power\">Power</label><div class=\"input-group\"> <input class=\"form-control input-sm\" type=\"text\" value=\"450.0\" name=\"factor[ns_p]\" id=\"factor_ns_p\"/> <div class=\"input-group-addon\"> W </div></div><label class=\"small_form\" for=\"daily_cost\">Daily cost</label><div class=\"input\"> <input type=\"text\" name=\"NeoScrypt daily cost\" id=\"NeoScrypt_daily_cost\" value=\"$1.08\" disabled=\"disabled\" class=\"form-control input-sm\"/></div></div><div class=\"col-xs-6\"> <div class=\"ck-button\"> <label> <input type=\"checkbox\" name=\"lbry\" id=\"lbry\" value=\"true\" hidden=\"hidden\"/> <span class=\"btn btn-default popover-as-title\" data-content=\"Include LBRY\" data-placement=\"bottom\" data-trigger=\"hover\"> LBRY </span> </label></div><label class=\"small_form\" for=\"hash_rate\">Hash rate</label><div class=\"input-group\"> <input class=\"form-control input-sm\" type=\"text\" value=\"315.0\" name=\"factor[lbry_hr]\" id=\"factor_lbry_hr\"/> <div class=\"input-group-addon\"> Mh/s </div></div><label class=\"small_form\" for=\"power\">Power</label><div class=\"input-group\"> <input class=\"form-control input-sm\" type=\"text\" value=\"525.0\" name=\"factor[lbry_p]\" id=\"factor_lbry_p\"/> <div class=\"input-group-addon\"> W </div></div><label class=\"small_form\" for=\"daily_cost\">Daily cost</label><div class=\"input\"> <input type=\"text\" name=\"LBRY daily cost\" id=\"LBRY_daily_cost\" value=\"$1.26\" disabled=\"disabled\" class=\"form-control input-sm\"/></div></div></div></div></div><div class=\"row\"> <div class=\"col-xs-3\" style=\"margin-top: 15px\"> <div class=\"row\"> <div class=\"col-xs-6\"> <div class=\"ck-button\"> <label> <input type=\"checkbox\" name=\"bk14\" id=\"bk14\" value=\"true\" hidden=\"hidden\"/> <span class=\"btn btn-default popover-as-title\" data-content=\"Include Blake (14r)\" data-placement=\"bottom\" data-trigger=\"hover\"> Blake (14r) </span> </label></div><label class=\"small_form\" for=\"hash_rate\">Hash rate</label><div class=\"input-group\"> <input class=\"form-control input-sm\" type=\"text\" value=\"5910.0\" name=\"factor[bk14_hr]\" id=\"factor_bk14_hr\"/> <div class=\"input-group-addon\"> Mh/s </div></div><label class=\"small_form\" for=\"power\">Power</label><div class=\"input-group\"> <input class=\"form-control input-sm\" type=\"text\" value=\"570.0\" name=\"factor[bk14_p]\" id=\"factor_bk14_p\"/> <div class=\"input-group-addon\"> W </div></div><label class=\"small_form\" for=\"daily_cost\">Daily cost</label><div class=\"input\"> <input type=\"text\" name=\"Blake (14r) daily cost\" id=\"Blake__14r__daily_cost\" value=\"$1.37\" disabled=\"disabled\" class=\"form-control input-sm\"/></div></div><div class=\"col-xs-6\"> <div class=\"ck-button\"> <label> <input type=\"checkbox\" name=\"pas\" id=\"pas\" value=\"true\" hidden=\"hidden\"/> <span class=\"btn btn-default popover-as-title\" data-content=\"Include Pascal\" data-placement=\"bottom\" data-trigger=\"hover\"> Pascal </span> </label></div><label class=\"small_form\" for=\"hash_rate\">Hash rate</label><div class=\"input-group\"> <input class=\"form-control input-sm\" type=\"text\" value=\"2100.0\" name=\"factor[pas_hr]\" id=\"factor_pas_hr\"/> <div class=\"input-group-addon\"> Mh/s </div></div><label class=\"small_form\" for=\"power\">Power</label><div class=\"input-group\"> <input class=\"form-control input-sm\" type=\"text\" value=\"405.0\" name=\"factor[pas_p]\" id=\"factor_pas_p\"/> <div class=\"input-group-addon\"> W </div></div><label class=\"small_form\" for=\"daily_cost\">Daily cost</label><div class=\"input\"> <input type=\"text\" name=\"Pascal daily cost\" id=\"Pascal_daily_cost\" value=\"$0.97\" disabled=\"disabled\" class=\"form-control input-sm\"/></div></div></div></div><div class=\"col-xs-3\" style=\"margin-top: 15px\"> <div class=\"row\"> <div class=\"col-xs-6\"> <div class=\"ck-button\"> <label> <input type=\"checkbox\" name=\"skh\" id=\"skh\" value=\"true\" hidden=\"hidden\"/> <span class=\"btn btn-default popover-as-title\" data-content=\"Include Skunkhash\" data-placement=\"bottom\" data-trigger=\"hover\"> Skunkhash </span> </label></div><label class=\"small_form\" for=\"hash_rate\">Hash rate</label><div class=\"input-group\"> <input class=\"form-control input-sm\" type=\"text\" value=\"54.0\" name=\"factor[skh_hr]\" id=\"factor_skh_hr\"/> <div class=\"input-group-addon\"> Mh/s </div></div><label class=\"small_form\" for=\"power\">Power</label><div class=\"input-group\"> <input class=\"form-control input-sm\" type=\"text\" value=\"345.0\" name=\"factor[skh_p]\" id=\"factor_skh_p\"/> <div class=\"input-group-addon\"> W </div></div><label class=\"small_form\" for=\"daily_cost\">Daily cost</label><div class=\"input\"> <input type=\"text\" name=\"Skunkhash daily cost\" id=\"Skunkhash_daily_cost\" value=\"$0.83\" disabled=\"disabled\" class=\"form-control input-sm\"/></div></div><div class=\"col-xs-6\"> <div class=\"ck-button\"> <label> <input type=\"checkbox\" name=\"n5\" id=\"n5\" value=\"true\" hidden=\"hidden\"/> <span class=\"btn btn-default popover-as-title\" data-content=\"Include NIST5\" data-placement=\"bottom\" data-trigger=\"hover\"> NIST5 </span> </label></div><label class=\"small_form\" for=\"hash_rate\">Hash rate</label><div class=\"input-group\"> <input class=\"form-control input-sm\" type=\"text\" value=\"57.0\" name=\"factor[n5_hr]\" id=\"factor_n5_hr\"/> <div class=\"input-group-addon\"> Mh/s </div></div><label class=\"small_form\" for=\"power\">Power</label><div class=\"input-group\"> <input class=\"form-control input-sm\" type=\"text\" value=\"345.0\" name=\"factor[n5_p]\" id=\"factor_n5_p\"/> <div class=\"input-group-addon\"> W </div></div><label class=\"small_form\" for=\"daily_cost\">Daily cost</label><div class=\"input\"> <input type=\"text\" name=\"NIST5 daily cost\" id=\"NIST5_daily_cost\" value=\"$0.83\" disabled=\"disabled\" class=\"form-control input-sm\"/></div></div></div></div><div class=\"col-xs-6\" style=\"margin-top: 15px\"> <div class=\"row\"> <div class=\"col-xs-3 hidden\"> <div class=\"ck-button\"> <label> <input type=\"checkbox\" name=\"l2z\" id=\"l2z\" value=\"true\" hidden=\"hidden\"/> <span class=\"btn btn-default popover-as-title\" data-content=\"Include Lyra2z\" data-placement=\"bottom\" data-trigger=\"hover\"> Lyra2z </span> </label></div><label class=\"small_form\" for=\"hash_rate\">Hash rate</label><div class=\"input-group\"> <input class=\"form-control input-sm\" type=\"text\" value=\"420.0\" name=\"factor[l2z_hr]\" id=\"factor_l2z_hr\"/> <div class=\"input-group-addon\"> kh/s </div></div><label class=\"small_form\" for=\"power\">Power</label><div class=\"input-group\"> <input class=\"form-control input-sm\" type=\"text\" value=\"300.0\" name=\"factor[l2z_p]\" id=\"factor_l2z_p\"/> <div class=\"input-group-addon\"> W </div></div><label class=\"small_form\" for=\"daily_cost\">Daily cost</label><div class=\"input\"> <input type=\"text\" name=\"Lyra2z daily cost\" id=\"Lyra2z_daily_cost\" value=\"$0.72\" disabled=\"disabled\" class=\"form-control input-sm\"/></div></div><div class=\"col-xs-9\" style=\"margin-top: -10px\"> <div class=\"row\"> <div class=\"col-xs-6\"> <label for=\"factor_cost\">Cost</label> <div class=\"input-group\"> <input class=\"form-control input-sm\" type=\"text\" value=\"0.1\" name=\"factor[cost]\" id=\"factor_cost\"/> <div class=\"input-group-addon\"> $/kWh </div></div></div><div class=\"col-xs-6\"> <label for=\"factor_sort_by\">Sort by</label> <select name=\"sort\" id=\"sort\" class=\"chosen-select\"><option value=\"Difficulty\">Difficulty</option><option value=\"NetHash\">NetHash</option><option value=\"EstimatedRewards\">Estimated Rewards</option><option value=\"EstimatedRewards24\">Estimated Rewards 24</option><option value=\"MarketCap\">Market Cap</option><option value=\"ExchangeVolume\">Exchange Volume</option><option value=\"Revenue\">Current revenue</option><option value=\"Profit\">Current profit</option><option selected=\"selected\" value=\"Profitability24\">Profitability 24h</option><option value=\"Profitability3\">Profitability 3 days</option><option value=\"Profitability7\">Profitability 7 days</option></select> </div></div><div class=\"row\" style=\"margin-bottom: 5px\"> <div class=\"col-xs-6\"> <label for=\"factor_volume_filter\">Volume filter</label> <select name=\"volume\" id=\"volume\" class=\"chosen-select\"><option selected=\"selected\" value=\"0\">Any volume</option><option value=\"0_1\">Volume &gt; 0.1</option><option value=\"0_5\">Volume &gt; 0.5</option><option value=\"1\">Volume &gt; 1</option><option value=\"5\">Volume &gt; 5</option><option value=\"10\">Volume &gt; 10</option><option value=\"50\">Volume &gt; 50</option><option value=\"100\">Volume &gt; 100</option></select> </div><div class=\"col-xs-6\"> <label for=\"factor_difficulty_for_revenue\">Difficulty for revenue</label> <select name=\"revenue\" id=\"revenue\" class=\"chosen-select\"><option value=\"current\">Current difficulty</option><option selected=\"selected\" value=\"24h\">Average last 24h</option><option value=\"3d\">Average last 3 days</option><option value=\"7d\">Average last 7 days</option></select> </div></div><div class=\"row\" style=\"margin-bottom: 5px\"> <div class=\"col-xs-12\"> <label for=\"factor_selected_exchanges\">Selected exchanges</label> <input name=\"factor[exchanges][]\" type=\"hidden\" value=\"\"/><select class=\"chosen-select\" multiple=\"multiple\" name=\"factor[exchanges][]\" id=\"factor_exchanges\"><option selected=\"selected\" value=\"abucoins\">Abucoins</option><option selected=\"selected\" value=\"bitfinex\">Bitfinex</option><option selected=\"selected\" value=\"bittrex\">Bittrex</option><option value=\"binance\">Binance</option><option selected=\"selected\" value=\"cryptopia\">Cryptopia</option><option selected=\"selected\" value=\"hitbtc\">HitBTC</option><option selected=\"selected\" value=\"poloniex\">Poloniex</option><option selected=\"selected\" value=\"yobit\">YoBit</option></select> </div></div><div class=\"row\" style=\"margin-bottom: 5px\"> <input type=\"hidden\" name=\"dataset\" id=\"dataset\" value=\"Main\"/> <div class=\"col-xs-6\"> <input type=\"submit\" name=\"commit\" value=\"Calculate\" class=\"btn btn-primary btn-block\" data-disable-with=\"Calculate\"/> </div><div class=\"col-xs-6\"> <a href=/coins?adapt_q_480=3&amp;adapt_480=true&amp;eth=true&amp;factor[eth_hr]=84.0&amp;factor[eth_p]=405.0&amp;grof=true&amp;factor[gro_hr]=63.9&amp;factor[gro_p]=450.0&amp;x11gf=true&amp;factor[x11g_hr]=20.1&amp;factor[x11g_p]=420.0&amp;cn=true&amp;factor[cn_hr]=2190.0&amp;factor[cn_p]=330.0&amp;eq=true&amp;factor[eq_hr]=870.0&amp;factor[eq_p]=360.0&amp;lre=true&amp;factor[lrev2_hr]=14700.0&amp;factor[lrev2_p]=390.0&amp;ns=true&amp;factor[ns_hr]=1950.0&amp;factor[ns_p]=450.0&amp;lbry=true&amp;factor[lbry_hr]=315.0&amp;factor[lbry_p]=525.0&amp;bk14=true&amp;factor[bk14_hr]=5910.0&amp;factor[bk14_p]=570.0&amp;pas=true&amp;factor[pas_hr]=2100.0&amp;factor[pas_p]=405.0&amp;skh=true&amp;factor[skh_hr]=54.0&amp;factor[skh_p]=345.0&amp;n5=true&amp;factor[n5_hr]=57.0&amp;factor[n5_p]=345.0&amp;factor[cost]=0.1&amp;sort=Profitability24&amp;volume=0&amp;revenue=24h&amp;factor[exchanges][]=&amp;factor[exchanges][]=abucoins&amp;factor[exchanges][]=bitfinex&amp;factor[exchanges][]=bittrex&amp;factor[exchanges][]=binance&amp;factor[exchanges][]=cryptopia&amp;factor[exchanges][]=hitbtc&amp;factor[exchanges][]=poloniex&amp;factor[exchanges][]=yobit&amp;dataset=Main class=\"btn btn-info btn-block\">Defaults</a> </div></div></div></div></div></div></form><h4 class=\"text-justify\">Using below table, you can check how profitable it is to mine selected altcoins in comparison to ethereum. Please note that calculations are based on mean values, therefore your final results may vary. For best results fill all fields with your hash rate and power consumption. Input Groestl hash rate, not Myriad-Groestl. Default values are adapted for three 480 cards.</h4><div class=\"centered-image\"> <ins class=\"adsbygoogle\" style=\"display:inline-block;width:728px;height:90px\" data-ad-client=\"ca-pub-2477602342455493\" data-ad-slot=\"8129567165\"></ins> <script>(adsbygoogle=window.adsbygoogle || []).push({});</script></div><table class=\"table table-hover table-vcenter\"> <thead> <tr> <th>Name(Tag)<br>Algorithm</th> <th>Block Time<br>Block Reward<br>Last Block</th> <th><div class=\"text-center\">Difficulty<br>NetHash<div></th> <th><div class=\"text-center\">Est. Rewards<br>Est. Rewards 24h</div></th> <th><div class=\"text-center\">Exchange Rate</div></th> <th>Market Cap<br>Volume</th> <th>Rev. BTC<br>Rev. 24h</th> <th>Rev. $<br>Profit</th> <th>Profitability<br>Current | 24h<br>3 days | 7 days</th> </tr></thead> <tbody> <tr class=success> <td> <div style=\"float: left\"> <img src=\"https://images.whattomine.com/coins/logos/000/000/151/thumb/eth.png?1486214743\" alt=\"Eth\" width=\"40\" height=\"40\"/> </div><div style=\"margin-left: 50px\"> <a href=\"/coins/151-eth-ethash\">Ethereum(ETH)</a> </div><div style=\"margin-left: 50px\"> Ethash </div></td><td> <div style=\"font-size: 8pt\"> BT: 14.66s<br>BR: 2.91<span class=\"glyphicon glyphicon-info-sign title-tip popover-as-title\" data-content=\"Reduced due to uncles lowered reward\" data-placement=\"right\" data-trigger=\"hover\"></span><br>LB: 5,153,447 </div></td><td> <div class=\"text-center\"> <strong> 3,064,162,143M </strong> <div class=\"small_text\"> 208.99 Th/s<br>-0.1% </div></div></td><td> <div class=\"text-center\"> 0.0069<br>0.0069 </div></td><td> <div class=\"text-center\"> <a target=\"_blank\" class=\"exrate-link\" href=\"https://poloniex.com/exchange#btc_eth\">0.08680000<br/>(Poloniex)</a> <div class=\"small_text\"> 0.9% </div></div></td><td> $82,581,221,875<br><strong> 2,425.87 BTC </strong> </td><td> 0.00060<br>0.00060 </td><td> $5.81<br><strong> $4.84 </strong> </td><td> 100% | <strong>100%</strong><br>100% | 100% </td></tr><tr > <td> <div style=\"float: left\"> <img src=\"https://images.whattomine.com/coins/logos/000/000/162/thumb/etc3.png?1496094431\" alt=\"Etc3\" width=\"40\" height=\"40\"/> </div><div style=\"margin-left: 50px\"> <a href=\"/coins/162-etc-ethash\">EthereumClassic(ETC)</a> </div><div style=\"margin-left: 50px\"> Ethash </div></td><td> <div style=\"font-size: 8pt\"> BT: 14.65s<br>BR: 3.88<span class=\"glyphicon glyphicon-info-sign title-tip popover-as-title\" data-content=\"Reduced due to uncles lowered reward\" data-placement=\"right\" data-trigger=\"hover\"></span><br>LB: 5,452,469 </div></td><td> <div class=\"text-center\"> <strong> 176,466,276M </strong> <div class=\"small_text\"> 12.05 Th/s<br>-2.1% </div></div></td><td> <div class=\"text-center\"> 0.1596<br>0.1562 </div></td><td> <div class=\"text-center\"> <a target=\"_blank\" class=\"exrate-link\" href=\"https://bittrex.com/Market/Index?MarketName=BTC-ETC\">0.00368800<br/>(Bittrex)</a> <div class=\"small_text\"> -1.1% </div></div></td><td> $3,590,350,207<br><strong> 1,544.91 BTC </strong> </td><td> 0.00059<br>0.00058 </td><td> $5.60<br><strong> $4.63 </strong> </td><td> <strong>98%</strong> | 96%<br>96% | 98% </td></tr><tr > <td> <div style=\"float: left\"> <img src=\"https://images.whattomine.com/nice_hash_coins/logos/000/000/015/thumb/nicehash2.png?1486215161\" alt=\"Nicehash2\" width=\"40\" height=\"40\"/> </div><div style=\"margin-left: 50px\"> Nicehash-Ethash </div><div style=\"margin-left: 50px\"> Ethash </div></td><td> <div style=\"font-size: 8pt\"> BT: -<br>BR: -<br>LB: - </div></td><td> <div class=\"text-center\"> <strong> - </strong> <div class=\"small_text\"> 4.72 Th/s<br>-13.0% </div></div></td><td> <div class=\"text-center\"> 0.00058<br>0.00057 </div></td><td> <div class=\"text-center\"> <a target=\"_blank\" class=\"exrate-link\" href=\"https://www.nicehash.com/?p=orders&amp;a=20\">0.00690000<br/>(Nicehash)</a> <div class=\"small_text\"> 1.5% </div></div></td><td> -<br><strong> 36.63 BTC </strong> </td><td> 0.00058<br>0.00057 </td><td> $5.55<br><strong> $4.58 </strong> </td><td> <strong>97%</strong> | 96%<br>93% | 92% </td></tr><tr > <td> <div style=\"float: left\"> <img src=\"https://images.whattomine.com/coins/logos/000/000/221/thumb/ella.png?1514372318\" alt=\"Ella\" width=\"40\" height=\"40\"/> </div><div style=\"margin-left: 50px\"> <a href=\"/coins/221-ella-ethash\">Ellaism(ELLA)</a> </div><div style=\"margin-left: 50px\"> Ethash </div></td><td> <div style=\"font-size: 8pt\"> BT: 14s<br>BR: 4.91<span class=\"glyphicon glyphicon-info-sign title-tip popover-as-title\" data-content=\"Reduced due to uncles lowered reward\" data-placement=\"right\" data-trigger=\"hover\"></span><br>LB: 1,028,242 </div></td><td> <div class=\"text-center\"> <strong> 4,128,436M </strong> <div class=\"small_text\"> 294.89 Gh/s<br>2.2% </div></div></td><td> <div class=\"text-center\"> 8.6291<br>8.8217 </div></td><td> <div class=\"text-center\"> <a target=\"_blank\" class=\"exrate-link\" href=\"https://www.cryptopia.co.nz/Exchange?market=ELLA_BTC\">0.00005869<br/>(Cryptopia)</a> <div class=\"small_text\"> -0.2% </div></div></td><td> $2,934,020<br><strong> 0.94 BTC </strong> </td><td> 0.00051<br>0.00052 </td><td> $5.03<br><strong> $4.06 </strong> </td><td> 85% | <strong>87%</strong><br>81% | 77% </td></tr><tr > <td> <div style=\"float: left\"> <img src=\"https://images.whattomine.com/coins/logos/000/000/209/thumb/etp.png?1508692794\" alt=\"Etp\" width=\"40\" height=\"40\"/> </div><div style=\"margin-left: 50px\"> <a href=\"/coins/209-etp-ethash\">Metaverse(ETP)</a> </div><div style=\"margin-left: 50px\"> Ethash </div></td><td> <div style=\"font-size: 8pt\"> BT: 30s<br>BR: 2.85<br>LB: 976,610 </div></td><td> <div class=\"text-center\"> <strong> 7,162,839M </strong> <div class=\"small_text\"> 238.76 Gh/s<br>6.6% </div></div></td><td> <div class=\"text-center\"> 2.8867<br>3.0762 </div></td><td> <div class=\"text-center\"> <a target=\"_blank\" class=\"exrate-link\" href=\"https://www.bitfinex.com/trading/ETPBTC\">0.00016200<br/>(Bitfinex)</a> <div class=\"small_text\"> -0.5% </div></div></td><td> $58,954,641<br><strong> 6.15 BTC </strong> </td><td> 0.00047<br>0.00050 </td><td> $4.85<br><strong> $3.87 </strong> </td><td> 78% | <strong>83%</strong><br>81% | 77% </td></tr><tr > <td> <div style=\"float: left\"> <img src=\"https://images.whattomine.com/coins/logos/000/000/178/thumb/music.png?1490568984\" alt=\"Music\" width=\"40\" height=\"40\"/> </div><div style=\"margin-left: 50px\"> <a href=\"/coins/178-music-ethash\">Musicoin(MUSIC)</a> </div><div style=\"margin-left: 50px\"> Ethash </div></td><td> <div style=\"font-size: 8pt\"> BT: 14s<br>BR: 246.20<span class=\"glyphicon glyphicon-info-sign title-tip popover-as-title\" data-content=\"Reduced due to uncles lowered reward\" data-placement=\"right\" data-trigger=\"hover\"></span><br>LB: 2,035,957 </div></td><td> <div class=\"text-center\"> <strong> 6,520,308M </strong> <div class=\"small_text\"> 465.74 Gh/s<br>-2.6% </div></div></td><td> <div class=\"text-center\"> 273.9899<br>266.9250 </div></td><td> <div class=\"text-center\"> <a target=\"_blank\" class=\"exrate-link\" href=\"https://bittrex.com/Market/Index?MarketName=BTC-MUSIC\">0.00000186<br/>(Bittrex)</a> <div class=\"small_text\"> 0.4% </div></div></td><td> $11,562,853<br><strong> 4.82 BTC </strong> </td><td> 0.00051<br>0.00050 </td><td> $4.83<br><strong> $3.86 </strong> </td><td> <strong>85%</strong> | 83%<br>80% | 79% </td></tr><tr > <td> <div style=\"float: left\"> <img src=\"https://images.whattomine.com/coins/logos/000/000/211/thumb/pirl.png?1509542465\" alt=\"Pirl\" width=\"40\" height=\"40\"/> </div><div style=\"margin-left: 50px\"> <a href=\"/coins/211-pirl-ethash\">Pirl(PIRL)</a> </div><div style=\"margin-left: 50px\"> Ethash </div></td><td> <div style=\"font-size: 8pt\"> BT: 14s<br>BR: 10.00<br>LB: 906,945 </div></td><td> <div class=\"text-center\"> <strong> 10,771,908M </strong> <div class=\"small_text\"> 769.42 Gh/s<br>-3.4% </div></div></td><td> <div class=\"text-center\"> 6.7368<br>6.5066 </div></td><td> <div class=\"text-center\"> <a target=\"_blank\" class=\"exrate-link\" href=\"https://www.cryptopia.co.nz/Exchange?market=PIRL_BTC\">0.00007541<br/>(Cryptopia)</a> <div class=\"small_text\"> -7.8% </div></div></td><td> $7,980,348<br><strong> 2.05 BTC </strong> </td><td> 0.00051<br>0.00049 </td><td> $4.77<br><strong> $3.80 </strong> </td><td> <strong>85%</strong> | 82%<br>78% | 76% </td></tr><tr><td colspan=\"9\"><div class='centered-image cointraffic-link'><span id='ct_c1CjxE'></span></div></td></tr><tr > <td> <div style=\"float: left\"> <img src=\"https://images.whattomine.com/coins/logos/000/000/154/thumb/exp2.png?1486175482\" alt=\"Exp2\" width=\"40\" height=\"40\"/> </div><div style=\"margin-left: 50px\"> <a href=\"/coins/154-exp-ethash\">Expanse(EXP)</a> </div><div style=\"margin-left: 50px\"> Ethash </div></td><td> <div style=\"font-size: 8pt\"> BT: 43s<br>BR: 4.00<br>LB: 1,002,413 </div></td><td> <div class=\"text-center\"> <strong> 16,952,905M </strong> <div class=\"small_text\"> 394.25 Gh/s<br>1.3% </div></div></td><td> <div class=\"text-center\"> 1.7120<br>1.7345 </div></td><td> <div class=\"text-center\"> <a target=\"_blank\" class=\"exrate-link\" href=\"https://bittrex.com/Market/?MarketName=BTC-EXP\">0.00025881<br/>(Bittrex)</a> <div class=\"small_text\"> -2.4% </div></div></td><td> $19,899,212<br><strong> 14.62 BTC </strong> </td><td> 0.00044<br>0.00045 </td><td> $4.37<br><strong> $3.39 </strong> </td><td> 74% | <strong>75%</strong><br>74% | 71% </td></tr><tr > <td> <div style=\"float: left\"> <img src=\"https://images.whattomine.com/coins/logos/000/000/173/thumb/ubq.png?1486175435\" alt=\"Ubq\" width=\"40\" height=\"40\"/> </div><div style=\"margin-left: 50px\"> <a href=\"/coins/173-ubq-ethash\">Ubiq(UBQ)</a> </div><div style=\"margin-left: 50px\"> Ethash </div></td><td> <div style=\"font-size: 8pt\"> BT: 1m 24s<br>BR: 7.00<br>LB: 390,855 </div></td><td> <div class=\"text-center\"> <strong> 28,244,868M </strong> <div class=\"small_text\"> 336.25 Gh/s<br>9.9% </div></div></td><td> <div class=\"text-center\"> 1.7982<br>1.9757 </div></td><td> <div class=\"text-center\"> <a target=\"_blank\" class=\"exrate-link\" href=\"https://bittrex.com/Market/Index?MarketName=BTC-UBQ\">0.00022572<br/>(Bittrex)</a> <div class=\"small_text\"> -1.0% </div></div></td><td> $87,287,272<br><strong> 13.45 BTC </strong> </td><td> 0.00041<br>0.00045 </td><td> $4.34<br><strong> $3.36 </strong> </td><td> 68% | <strong>75%</strong><br>70% | 69% </td></tr><tr><td colspan=\"9\"><div class=\"centered-image\"><a target=\"_blank\" class=\"blockchain-link\" href=\"http://blockchainweekly.net/?utm_source=whattomine&amp;utm_campaign=bottom_popup&amp;utm_medium=banner\"><img src=\"https://images.whattomine.com/misc/weekly2.png\" alt=\"Weekly2\"/></a></div></td></tr></tbody></table>Last update at 2018-02-25 11:15:54 UTC <footer class=\"footer\"> <nav> <small> <ul> © 2018 <a href=\"/coins\">WhatToMine</a> | <a target=\"_blank\" href=\"https://bitcointalk.org/index.php?topic=567730.0\">Bitcointalk Thread</a> | <a href=\"/policy-terms\">Policy &amp; Terms</a> </ul> </small> </nav></footer> </div></body></html>";
            string tableRows = GetTextFromHtmlStringByCssSelectorTest(htmlResponse, "table tbody tr");
            //string tableRowss = GetRevAndProfit(htmlResponse, "table tbody tr"," ");

            //gpu.Hashrate = double.Parse(hashRateText);

        }


        private static string GetTextFromHtmlStringByCssSelectorTest(string response, string CssSelector)
        {
            CQ dom = response;
            CQ textElement = dom[CssSelector];
            string nodeName = textElement.FirstElement().NodeName.ToLower();
            string result = textElement.Text(); ;

            if (nodeName == "input")
                result = textElement.Val();
            return result;
        }

    }
    //public static class Insighter
    //{
    //    public static List<GPU> Gpus = new List<GPU>();
    //    internal static List<GPU> GetInsights()
    //    {
    //        //seed initial data
    //        LoadData();
    //        //Gather information
    //        GatherInfo(Gpus);
    //        // For each GPU ,calculate Cost / 1 MHs/s  For Each Source
    //        FindHashCost(Gpus);
    //        // sort by that value
    //        Gpus = Gpus.OrderByDescending(g => g.ProfitPerYearMinusCostUsd).ToList();
    //        return Gpus;
    //    }

    //    private static void FindHashCost(List<GPU> gpus)
    //    {
    //        gpus.ForEach(FindHashCost);
    //    }
    //    private static void FindHashCost(GPU gpu)
    //    {
    //        gpu.HashPricePerSourceList.Clear();

    //        foreach (var item in gpu.PriceSources)
    //        {
    //            //price for the GPU / Hashrate
    //            HashPricePerSource hashPricePerSource = new HashPricePerSource()
    //            {
    //                Source = item.Name,
    //                HashPrice = item.Price / gpu.Hashrate
    //            };
    //            gpu.HashPricePerSourceList.Add(hashPricePerSource);
    //        }
    //    }

    //    private static void GatherInfo(List<GPU> gpus)
    //    {
    //        foreach (var gpu in gpus)
    //        {
    //            System.Console.WriteLine($"Getting data for GPU {gpu.Name}");

    //            GetPrices(gpu);
    //            GetHashrate(gpu);
    //            GeRevenueAndProfit(gpu, "table tbody tr");
    //            GetMiningProfitability(gpu);
    //        }
    //    }

    //    private static void GetHashrate(GPU gpu)
    //    {
    //        System.Console.WriteLine($"Getting Hashrate From WhatToMine For GPU {gpu.Name}");

    //        string response = GetHttpResponseText(gpu.WhatToMineUrl);
    //        string hashRateText = GetTextFromHtmlStringByCssSelector(response, "#factor_eth_hr");
    //        gpu.Hashrate = double.Parse(hashRateText);
    //    }
    //    //private static string GeRevenueAndProfit(string response, string CssSelector, string targetCurrency)
    //    //{
    //    //    CQ dom = response;
    //    //    CQ rows = dom[CssSelector];
    //    //    foreach (var row in rows)
    //    //    {
    //    //        var firstColumn = row.ChildElements.FirstOrDefault();
    //    //        CQ anchor = ((CQ)firstColumn.InnerHTML)["div:nth-child(2) a"];
    //    //        string cryptoname = anchor.Text();
    //    //        if (cryptoname != targetCurrency) continue;
    //    //        var seventhColumn = row.ChildElements.ElementAt(7);
    //    //        string revenueText = seventhColumn.InnerText;
    //    //        //second element bcoz of the <br />
    //    //        string profitText = seventhColumn.ChildElements.ElementAt(1).InnerText;

    //    //    }
    //    //    //string nodeName = textElement.FirstElement().NodeName.ToLower();
    //    //    //string result = textElement.Text(); ;

    //    //    //if (nodeName == "input")
    //    //    //    result = textElement.Val();
    //    //    return "";
    //    //}
    //    private static string GeRevenueAndProfit(GPU gpu,string CssSelector)
    //    {
    //            System.Console.WriteLine($"Getting Revenu and profit From What to mine For GPU {gpu.Name}");
    //        string response = GetHttpResponseText(gpu.WhatToMineUrl);
    //        CQ dom = response;
    //        CQ rows = dom[CssSelector];
    //        foreach (var row in rows)
    //        {
    //            var firstColumn = row.ChildElements.FirstOrDefault();
    //            CQ anchor = ((CQ)firstColumn.InnerHTML)["div:nth-child(2) a"];
    //            string cryptoname = anchor.Text();
    //            if (cryptoname != gpu.CoinToStudyName) continue;
    //            var seventhColumn = row.ChildElements.ElementAt(7);
    //            string revenueText = seventhColumn.InnerText;
    //            //second element bcoz of the <br />
    //            string profitText = seventhColumn.ChildElements.ElementAt(1).InnerText;
    //            gpu.RevenuePerDayUsd = double.Parse(revenueText.Replace("$", ""));
    //            gpu.ProfitPerDayUsd = double.Parse(profitText.Replace("$", ""));
    //        }
    //        //string nodeName = textElement.FirstElement().NodeName.ToLower();
    //        //string result = textElement.Text(); ;

    //        //if (nodeName == "input")
    //        //    result = textElement.Val();
    //        return "";
    //    }
    //    private static void GetMiningProfitability(GPU gpu)
    //    {
    //        System.Console.WriteLine($"Getting Mining Profitability From What to mine based on {gpu.CoinToStudyName} For GPU {gpu.Name}");

    //        //https://whattomine.com/coins?utf8=%E2%9C%93&adapt_q_280x=0&adapt_q_380=0&adapt_q_fury=0&adapt_q_470=0&adapt_q_480=3&adapt_q_570=1&adapt_570=true&adapt_q_580=0&adapt_q_vega56=0&adapt_q_vega64=0&adapt_q_750Ti=0&adapt_q_1050Ti=0&adapt_q_10606=0&adapt_q_1070=0&adapt_q_1070Ti=0&adapt_q_1080=0&adapt_q_1080Ti=0&eth=true&factor%5Beth_hr%5D=27.9&factor%5Beth_p%5D=120.0&factor%5Bgro_hr%5D=15.5&factor%5Bgro_p%5D=110.0&factor%5Bx11g_hr%5D=5.6&factor%5Bx11g_p%5D=110.0&factor%5Bcn_hr%5D=700.0&factor%5Bcn_p%5D=110.0&factor%5Beq_hr%5D=260.0&factor%5Beq_p%5D=110.0&factor%5Blrev2_hr%5D=5500.0&factor%5Blrev2_p%5D=110.0&factor%5Bns_hr%5D=630.0&factor%5Bns_p%5D=140.0&factor%5Blbry_hr%5D=115.0&factor%5Blbry_p%5D=115.0&factor%5Bbk2b_hr%5D=840.0&factor%5Bbk2b_p%5D=115.0&factor%5Bbk14_hr%5D=1140.0&factor%5Bbk14_p%5D=115.0&factor%5Bpas_hr%5D=580.0&factor%5Bpas_p%5D=135.0&factor%5Bskh_hr%5D=16.3&factor%5Bskh_p%5D=110.0&factor%5Bn5_hr%5D=18.0&factor%5Bn5_p%5D=110.0&factor%5Bl2z_hr%5D=420.0&factor%5Bl2z_p%5D=300.0&factor%5Bcost%5D=0.1&sort=Profitability24&volume=0&revenue=24h&factor%5Bexchanges%5D%5B%5D=&factor%5Bexchanges%5D%5B%5D=abucoins&factor%5Bexchanges%5D%5B%5D=bitfinex&factor%5Bexchanges%5D%5B%5D=bittrex&factor%5Bexchanges%5D%5B%5D=binance&factor%5Bexchanges%5D%5B%5D=cryptopia&factor%5Bexchanges%5D%5B%5D=hitbtc&factor%5Bexchanges%5D%5B%5D=poloniex&factor%5Bexchanges%5D%5B%5D=yobit&dataset=&commit=Calculate
    //        string response = GetHttpResponseText(gpu.WhatToMineUrl);
    //        // get the table

    //        // get the first row
    //        // first column
    //        // crypto name
    //        string cryptoName = GetTextFromHtmlStringByCssSelector(response, "body table tbody tr:first td:first div:nth-child(2)");

    //        //algo
    //        string cryptoAlgo = GetTextFromHtmlStringByCssSelector(response, "body table tbody tr:first td:first div:nth-child(3)");
    //        //8th column second line, profitability
    //        //$('').text()
    //        string profitability = GetTextFromHtmlStringByCssSelector(response, "body table tbody tr:first td:nth-child(8) strong");
    //        gpu.MiningProfitability.CryptoName = cryptoName;
    //        gpu.MiningProfitability.CryptoAlgo = cryptoAlgo;
    //        string profitabilityText = profitability;
    //        if (profitabilityText.IndexOf("$") > -1)
    //        {
    //            profitabilityText = profitabilityText.Remove(profitabilityText.IndexOf("$"), 1);
    //        }
    //        gpu.MiningProfitability.Profitability24Hours = double.Parse(profitabilityText);
    //    }

    //    private static string GetTextFromHtmlStringByCssSelector(string response, string CssSelector)
    //    {
    //        CQ dom = response;
    //        CQ textElement = dom[CssSelector];
    //        string nodeName = textElement.FirstElement().NodeName.ToLower();
    //        string result = textElement.Text(); ;

    //        if (nodeName == "input")
    //            result = textElement.Val();
    //        return result;
    //    }
    //    private static string GetHttpResponseTextWithJavascript(string url)
    //    {
    //        var options = new PhantomJSOptions();
    //        var driver = new PhantomJSDriver(options);
    //        driver.Manage().Window.Size = new Size(1360, 728);
    //        var size = driver.Manage().Window.Size;

    //        driver.Navigate().GoToUrl(url);
    //        //string url = driver.Url;
    //        //the driver can now provide you with what you need (it will execute the script)
    //        //get the source of the page
    //        var source = driver.PageSource;
    //        //fully navigate the dom
    //        source = driver.PageSource;
    //        return source;
    //    }
    //    private static string GetHttpResponseText(string url)
    //    {
    //            var request = (HttpWebRequest)WebRequest.Create(url);
    //            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";


    //        using (var response = (HttpWebResponse)request.GetResponse())
    //            {
    //                var encoding = Encoding.GetEncoding(response.CharacterSet);

    //                using (var responseStream = response.GetResponseStream())
    //                using (var reader = new StreamReader(responseStream, encoding))
    //                    return reader.ReadToEnd();
    //            }
    //    }
    //    //private static string GetHttpResponseText(string url)
    //    //{
    //    //    HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

    //    //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

    //    //    WebHeaderCollection header = response.Headers;

    //    //    var encoding = ASCIIEncoding.ASCII;
    //    //    string responseText = string.Empty;
    //    //    using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
    //    //    {
    //    //        responseText = reader.ReadToEnd();
    //    //    }

    //    //    return responseText;
    //    //}

    //    private static void GetPrices(GPU gpu)
    //    {
    //        foreach (var priceSource in gpu.PriceSources)
    //        {
    //            System.Console.WriteLine($"Getting Price From {priceSource.Name} For GPU {gpu.Name}");

    //            string response = GetHttpResponseTextWithJavascript(priceSource.URL);
    //            string PriceText = GetTextFromHtmlStringByCssSelector(response, priceSource.Selector);
    //            if (PriceText.IndexOf("$") > -1)
    //            {
    //                PriceText = PriceText.Remove(PriceText.IndexOf("$"), 1);
    //            }
    //            priceSource.Price = double.Parse(PriceText);
    //        }
    //    }
    //    //aaaa ssss

    //    private static void LoadData()
    //    {
    //        Gpus.Clear();
    //        Gpus = new List<GPU> {
    //            new GPU()
    //            {
    //                Name = "RX 570",
    //                CoinToStudyName="Ethereum(ETH)",
    //                WhatToMineUrl = "https://whattomine.com/coins?utf8=%E2%9C%93&adapt_q_280x=0&adapt_q_380=0&adapt_q_fury=0&adapt_q_470=0&adapt_q_480=3&adapt_q_570=1&adapt_570=true&adapt_q_580=0&adapt_q_vega56=0&adapt_q_vega64=0&adapt_q_750Ti=0&adapt_q_1050Ti=0&adapt_q_10606=0&adapt_q_1070=0&adapt_q_1070Ti=0&adapt_q_1080=0&adapt_q_1080Ti=0&eth=true&factor%5Beth_hr%5D=27.9&factor%5Beth_p%5D=120.0&factor%5Bgro_hr%5D=15.5&factor%5Bgro_p%5D=110.0&factor%5Bx11g_hr%5D=5.6&factor%5Bx11g_p%5D=110.0&factor%5Bcn_hr%5D=700.0&factor%5Bcn_p%5D=110.0&factor%5Beq_hr%5D=260.0&factor%5Beq_p%5D=110.0&factor%5Blrev2_hr%5D=5500.0&factor%5Blrev2_p%5D=110.0&factor%5Bns_hr%5D=630.0&factor%5Bns_p%5D=140.0&factor%5Blbry_hr%5D=115.0&factor%5Blbry_p%5D=115.0&factor%5Bbk2b_hr%5D=840.0&factor%5Bbk2b_p%5D=115.0&factor%5Bbk14_hr%5D=1140.0&factor%5Bbk14_p%5D=115.0&factor%5Bpas_hr%5D=580.0&factor%5Bpas_p%5D=135.0&factor%5Bskh_hr%5D=16.3&factor%5Bskh_p%5D=110.0&factor%5Bn5_hr%5D=18.0&factor%5Bn5_p%5D=110.0&factor%5Bl2z_hr%5D=420.0&factor%5Bl2z_p%5D=300.0&factor%5Bcost%5D=0.1&sort=Profitability24&volume=0&revenue=24h&factor%5Bexchanges%5D%5B%5D=&factor%5Bexchanges%5D%5B%5D=abucoins&factor%5Bexchanges%5D%5B%5D=bitfinex&factor%5Bexchanges%5D%5B%5D=bittrex&factor%5Bexchanges%5D%5B%5D=binance&factor%5Bexchanges%5D%5B%5D=cryptopia&factor%5Bexchanges%5D%5B%5D=hitbtc&factor%5Bexchanges%5D%5B%5D=poloniex&factor%5Bexchanges%5D%5B%5D=yobit&dataset=&commit=Calculate",
    //                PriceSources = new List<PriceSource>(){new PriceSource(){Name="New Egg",URL="https://www.newegg.com/Product/Product.aspx?Item=9SIA6V661W8467&cm_re=rx_570-_-9SIA6V661W8467-_-Product",RequiresJavascript=true,Selector="#landingpage-price .price-current"}
    //                }
    //            },
    //              new GPU()
    //              {
    //                  Name = "RX 480",
    //                  WhatToMineUrl = "https://whattomine.com/coins?utf8=%E2%9C%93&adapt_q_280x=0&adapt_q_380=0&adapt_q_fury=0&adapt_q_470=0&adapt_q_480=1&adapt_480=true&adapt_q_570=0&adapt_q_580=0&adapt_q_vega56=0&adapt_q_vega64=0&adapt_q_750Ti=0&adapt_q_1050Ti=0&adapt_q_10606=0&adapt_q_1070=0&adapt_q_1070Ti=0&adapt_q_1080=0&adapt_q_1080Ti=0&eth=true&factor%5Beth_hr%5D=29.5&factor%5Beth_p%5D=135.0&factor%5Bgro_hr%5D=18.0&factor%5Bgro_p%5D=130.0&factor%5Bx11g_hr%5D=6.7&factor%5Bx11g_p%5D=140.0&factor%5Bcn_hr%5D=730.0&factor%5Bcn_p%5D=110.0&factor%5Beq_hr%5D=290.0&factor%5Beq_p%5D=120.0&factor%5Blrev2_hr%5D=4900.0&factor%5Blrev2_p%5D=130.0&factor%5Bns_hr%5D=650.0&factor%5Bns_p%5D=150.0&factor%5Blbry_hr%5D=95.0&factor%5Blbry_p%5D=140.0&factor%5Bbk2b_hr%5D=990.0&factor%5Bbk2b_p%5D=150.0&factor%5Bbk14_hr%5D=1400.0&factor%5Bbk14_p%5D=150.0&factor%5Bpas_hr%5D=690.0&factor%5Bpas_p%5D=135.0&factor%5Bskh_hr%5D=18.0&factor%5Bskh_p%5D=115.0&factor%5Bn5_hr%5D=19.0&factor%5Bn5_p%5D=115.0&factor%5Bl2z_hr%5D=420.0&factor%5Bl2z_p%5D=300.0&factor%5Bcost%5D=0.1&sort=Profitability24&volume=0&revenue=24h&factor%5Bexchanges%5D%5B%5D=&factor%5Bexchanges%5D%5B%5D=abucoins&factor%5Bexchanges%5D%5B%5D=bitfinex&factor%5Bexchanges%5D%5B%5D=bittrex&factor%5Bexchanges%5D%5B%5D=binance&factor%5Bexchanges%5D%5B%5D=cryptopia&factor%5Bexchanges%5D%5B%5D=hitbtc&factor%5Bexchanges%5D%5B%5D=poloniex&factor%5Bexchanges%5D%5B%5D=yobit&dataset=Main&commit=Calculate",
    //                  PriceSources = new List<PriceSource>(){new PriceSource(){
    //                      Name="New Egg",
    //                      URL="https://www.newegg.com/Product/Product.aspx?Item=9SIA6V66TC9897&cm_re=rx_480-_-9SIA6V66TC9897-_-Product",
    //                      Selector="#landingpage-price .price-current"
    //                  ,RequiresJavascript=true}
    //                }
    //              },
    //              new GPU()
    //              {
    //                  Name = "R9 380",
    //                  WhatToMineUrl = "https://whattomine.com/coins?utf8=%E2%9C%93&adapt_q_280x=0&adapt_q_380=1&adapt_380=true&adapt_q_fury=0&adapt_q_470=0&adapt_q_480=0&adapt_q_570=0&adapt_q_580=0&adapt_q_vega56=0&adapt_q_vega64=0&adapt_q_750Ti=0&adapt_q_1050Ti=0&adapt_q_10606=0&adapt_q_1070=0&adapt_q_1070Ti=0&adapt_q_1080=0&adapt_q_1080Ti=0&eth=true&factor%5Beth_hr%5D=20.2&factor%5Beth_p%5D=145.0&factor%5Bgro_hr%5D=15.5&factor%5Bgro_p%5D=130.0&factor%5Bx11g_hr%5D=2.5&factor%5Bx11g_p%5D=120.0&factor%5Bcn_hr%5D=530.0&factor%5Bcn_p%5D=120.0&factor%5Beq_hr%5D=205.0&factor%5Beq_p%5D=130.0&factor%5Blrev2_hr%5D=6400.0&factor%5Blrev2_p%5D=125.0&factor%5Bns_hr%5D=350.0&factor%5Bns_p%5D=145.0&factor%5Blbry_hr%5D=44.0&factor%5Blbry_p%5D=135.0&factor%5Bbk2b_hr%5D=760.0&factor%5Bbk2b_p%5D=150.0&factor%5Bbk14_hr%5D=1140.0&factor%5Bbk14_p%5D=155.0&factor%5Bpas_hr%5D=480.0&factor%5Bpas_p%5D=145.0&factor%5Bskh_hr%5D=9.0&factor%5Bskh_p%5D=120.0&factor%5Bn5_hr%5D=10.5&factor%5Bn5_p%5D=120.0&factor%5Bl2z_hr%5D=420.0&factor%5Bl2z_p%5D=300.0&factor%5Bcost%5D=0.1&sort=Profitability24&volume=0&revenue=24h&factor%5Bexchanges%5D%5B%5D=&factor%5Bexchanges%5D%5B%5D=abucoins&factor%5Bexchanges%5D%5B%5D=bitfinex&factor%5Bexchanges%5D%5B%5D=bittrex&factor%5Bexchanges%5D%5B%5D=binance&factor%5Bexchanges%5D%5B%5D=cryptopia&factor%5Bexchanges%5D%5B%5D=hitbtc&factor%5Bexchanges%5D%5B%5D=poloniex&factor%5Bexchanges%5D%5B%5D=yobit&dataset=Main&commit=Calculate",
    //                  PriceSources = new List<PriceSource>(){new PriceSource(){
    //                      Name="New Egg",
    //                      URL="https://www.newegg.com/Product/Product.aspx?Item=9SIA85V6DW3604",
    //                      Selector="#landingpage-price .price-current"
    //                  ,RequiresJavascript=true}
    //                }
    //              }
    //              };
    //    }
    //}

}
