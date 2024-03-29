﻿<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

<title>FusionCharts v3.2 ASP.NET(C#) Samples</title>

<link href="assets/ui/css/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="assets/ui/js/jquery-1.4.2.min.js"></script>


<style type="text/css">
	h2.headline {
		font: normal 110%/137.5% "Trebuchet MS", Arial, Helvetica, sans-serif;
		padding: 0;
		margin: 15px 0 15px 0;
		color: #000000;
	}
	
	ul.nolist {list-style : none;}
	
	.underline-dull { height:10px;}
</style>

</head>
<body>
<!-- wrapper -->
<div id="wrapper">
  <!-- header -->
  <div id="header">
    <div class="back-to-home" style="visibility:hidden;"><a href="../Default.aspx">Back to home</a></div>

	 <div class="logo"><a href="http://www.fusioncharts.com"><img src="assets/ui/images/fusionchartsv3.2-logo.png" width="131" height="75" alt="FusionCharts v3.2 logo" /></a></div>
    <h1 class="logo-text">FusionCharts ASP.NET(C#) Samples</h1>
</div>
  <!-- content area -->
  <div class="content-area">
  <div id="content-area-inner-main">
  <!--

  -->
	 <ul class="nolist">
		  <li><h2 class="headline">Basic Examples</h2>
			  <ul class="sec-arr-list">
					<li><a href="BasicExample/SimpleChart.aspx">Simple Column 3D chart using data from XML file (dataUrl method)</a></li>
					<li><a href="BasicExample/dataXML.aspx">Simple Column 3D chart with XML data hard-coded in aspx page (dataStr method)</a></li>
					<li><a href="BasicExample/SimpleChartJSON.aspx">Simple Column 3D chart using data from JSON file (dataUrl method)</a></li>
					<li><a href="BasicExample/dataStrJSON.aspx">Simple Column 3D chart with JSON data hard-coded in aspx page (dataStr method)</a></li>
					<li><a href="BasicExample/BasicChart.aspx">HTML Embedding using dataUrl method</a></li>
					<li><a href="BasicExample/BasicDataXML.aspx">HTML embedding using dataStr method</a></li>
					<li><a href="BasicExample/MultiChart.aspx">Multiple charts on a single page</a></li>
					<li><a href="BasicExample/MultiChartsJS.aspx">Creating pure JavaScript based charts</a></li>
					<li><a href="BasicExample/ManagedPrint.aspx">Using managed print for Mozilla browsers </a></li>
			      <li><a href="BasicExample/TransparentChart.aspx">Creating Transparent chart</a></li>
			  </ul>
			  <div class="underline-dull"></div>
		</li>
		
		  <li><h2 class="headline">Plotting Chart from Data Contained in Arrays</h2>
			  <ul class="sec-arr-list">
				<li><a href="ArrayExample/SingleSeries.aspx">Single Series chart example</a></li>
				<li><a href="ArrayExample/MultiSeries.aspx">Multi Series chart example</a></li>
				<li><a href="ArrayExample/Stacked.aspx">Stacked chart example</a></li>
				<li><a href="ArrayExample/Combination.aspx">Combination chart example</a></li>
				</ul><div class="underline-dull"></div>
		  </li>
		  
	 <li><h2 class="headline">Form Based Example</h2>
		  <ul class="sec-arr-list">
			  <li><a href="FormBased/Default.aspx">Plotting charts from data in forms</a></li>
			  </ul> <div class="underline-dull"></div>
	 </li>
	 
	 <li><h2 class="headline">Database Examples</h2>
		  <ul class="sec-arr-list">
			<li><a href="DBExample/BasicDBExample.aspx">Database example Using dataXML method</a></li>
			<li><a href="DB_dataURL/Default.aspx">Database example Using dataURL method</a></li>
			 <li><a href="DBExample/MSCharts.aspx">Multi-series line chart from database using dataURL method</a></li>
			</ul><div class="underline-dull"></div>
	 </li>
	 
	 <li><h2 class="headline">Database and Drill-down Examples</h2>
		  <ul class="sec-arr-list">
			 <li><a href="DBExample/Default.aspx">Basic Drill-down example</a></li>
			 <li><a href="DB_JS_dataURL/LinkedSimple.aspx">Linked charts</a></li>
			 <li><a href="DB_JS/Default.aspx">Client side dynamic chart example</a></li>
			 <li><a href="DB_JS_dataURL/Default.aspx">Dynamic chart example using dataUrl method</a></li>
			</ul>
		  <div class="underline-dull"></div>
	 </li>
	 <li><h2 class="headline">Exporting of charts</h2>
		 <ul class="sec-arr-list">
			 <li><a href="Export/Download.aspx">Download after export </a></li>
			 <li><a href="Export/Save.aspx">Save to a folder after export</a></li>
			 <li><a href="Export/MultipleCharts.aspx">Export multiple charts simultaneously</a></li>
			 <li><a href="Export/Automated.aspx">Automatic export</a></li>
		 </ul><div class="underline-dull"></div>
	 </li>
	 <li><h2 class="headline">Multilingual (UTF-8) Examples</h2>
			<ul class="sec-arr-list">
			  <li><a href="UTF8Examples/DataURL.aspx">Multilingual example using data from XML file</a></li>
			  <li><a href="UTF8Examples/DataXML.aspx">Multilingual example using data hard-coded in aspx file</a></li>
			  <li><a href="UTF8Examples/Database.aspx">Multilingual example using data from database</a></li>
			</ul>
		</li>
	 </ul>
	 <p>&nbsp;</p>
  <div class="underline-dull"></div>
  </div>
</div>
  <!-- footer -->
  <p>&nbsp;</p>
   <div id="footer">
    </div>
</div>
</body>
</html>
