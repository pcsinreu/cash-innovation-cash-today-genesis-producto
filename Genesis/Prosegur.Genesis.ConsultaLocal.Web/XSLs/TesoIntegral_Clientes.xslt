<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="xml" version="1.0" encoding="UTF-8" indent="yes" omit-xml-declaration="yes"/>
	<xsl:template match="/">
    <xsl:element name="div">
      <xsl:attribute name ="style">display:block;margin-left:100px;margin-right:100px;margin-top:5px;</xsl:attribute>
      <!--<xsl:attribute name ="style">display:block;width:400px;align:center;margin-top:5px;</xsl:attribute>-->
      <xsl:attribute name ="align">center</xsl:attribute>

      <xsl:element name="table">
        <xsl:attribute name ="align">center</xsl:attribute>
        <xsl:attribute name ="class">ui-datatable ui-datatable-data</xsl:attribute>
        <xsl:attribute name ="cellspacing">5</xsl:attribute>
        <xsl:attribute name ="style">border-style:None;width:98%;border-collapse:collapse;</xsl:attribute>
        <xsl:attribute name ="rules">all</xsl:attribute>
        <xsl:attribute name="border">1</xsl:attribute>
			  <xsl:apply-templates select="//Clientes"/>
      </xsl:element>
    </xsl:element>
	</xsl:template>

	
	<xsl:template match="Clientes">
		<xsl:element name="tr">
			<xsl:element name="td">
        <xsl:attribute name ="align">center</xsl:attribute>
				<xsl:element name="a">
					<xsl:attribute name="href">Consulta2.aspx?accion=OBTENER_SALDOS_CLIENTE_DELEGACION&amp;OID=<xsl:value-of select="./OID_CLIENTE"/></xsl:attribute>
					<xsl:element name="img">
						<xsl:attribute name="src"><xsl:value-of select="./DES_PATH_RELATIVO_LOGO"/>/<xsl:value-of select="./DES_ARCHIVO_LOGO"/></xsl:attribute>
						<xsl:attribute name="alt"><xsl:value-of select="./DES_CLIENTE"/></xsl:attribute>
						<xsl:attribute name="title"><xsl:value-of select="./DES_CLIENTE"/></xsl:attribute>
					</xsl:element>
				</xsl:element>
			</xsl:element>
		</xsl:element>
	</xsl:template>

</xsl:stylesheet>
