<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128534003/13.1.4%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E2405)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [Default.aspx](./CS/WebSite/Default.aspx) (VB: [Default.aspx](./VB/WebSite/Default.aspx))
* [Default.aspx.cs](./CS/WebSite/Default.aspx.cs) (VB: [Default.aspx.vb](./VB/WebSite/Default.aspx.vb))
<!-- default file list end -->
# ASPxGridView - How to edit XML file using XmlDataSource
<!-- run online -->
**[[Run Online]](https://codecentral.devexpress.com/e2405/)**
<!-- run online end -->


<p>The <a href="http://msdn.microsoft.com/en-us/library/system.web.ui.webcontrols.xmldatasource%28VS.80%29.aspx">XmlDataSource</a> control doesn't have the built-in API to edit an <strong>XML File</strong> specified via the XmlDataSource's <a href="http://msdn.microsoft.com/en-US/library/system.web.ui.webcontrols.xmldatasource.datafile%28v=VS.80%29.aspx">DataFile</a> property.</p><p>So, it's necessary to edit the <strong>XML File</strong> via the built-in .NET Framework's classes: XmlNode, XmlAttribute, etc.</p><p><strong>Note:</strong> As the <strong>XmlDataSource</strong> control doesn't have the built-in API to edit the <strong>XML File</strong>, the ASPxGridView control renders its editor as <strong>ReadOnly</strong>. So, it's necessary to enable the ASPxGridView's editors explicitely by handling the ASPxGridView's <a href="http://documentation.devexpress.com/#AspNet/DevExpressWebASPxGridViewASPxGridView_CellEditorInitializetopic">CellEditorInitialize</a> event.</p>

<br/>


