<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="IThemeSky.Management.Reports.test" %>
<?xml version="1.0" encoding="utf-8" ?><chart palette='1' caption='iSprit用户使用趋势图' showLabels='1' shownames='1' showvalues='1' decimals='0'  xAxisName='访问日期' yAxisName='用户数' useRoundEdges='1' legendBorderAlpha='0'>
<categories>
<category label='2010-05-31' />
<category label='2010-06-01' />
<category label='2010-06-02' />
<category label='2010-06-03' />
<category label='2010-06-04' />
<category label='2010-06-05' />
<category label='2010-06-06' />
<category label='2010-06-07' />
<category label='2010-06-08' />
<category label='2010-06-09' />
</categories>
<dataset seriesName='总用户' color='AFD8F8' showValues='0'>
<set value='88' />
<set value='76' />
<set value='86' />
<set value='76' />
<set value='92' />
<set value='155' />
<set value='155' />
<set value='111' />
<set value='114' />
<set value='299' />
</dataset>
<dataset seriesName='老用户' color='F6BD0F' showValues='0'>
<set value='14' />
<set value='13' />
<set value='7' />
<set value='19' />
<set value='25' />
<set value='22' />
<set value='30' />
<set value='13' />
<set value='25' />
<set value='31' />
</dataset>
</chart>