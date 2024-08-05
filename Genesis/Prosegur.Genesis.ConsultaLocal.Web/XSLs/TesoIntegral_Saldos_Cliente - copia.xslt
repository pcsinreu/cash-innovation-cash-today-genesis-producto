<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="xml" version="1.0" encoding="UTF-8" indent="yes" omit-xml-declaration="yes"/>
	<xsl:template match="/">
	
		<xsl:element name="div">
			<xsl:attribute name="class">content</xsl:attribute>

			<style>            
				.ColunaSemMargin {                
					margin: 0px !important;                
					padding: 0px !important;            
					}            
				.ColunaDivisa {                
					border-left-style: hidden;            
					}        
			</style>
			
			<xsl:element name="div">
				<xsl:attribute name="style">display:block;margin-left:10px;margin-top:5px;</xsl:attribute>
				
				<xsl:element name="table">
					<xsl:attribute name="class">ui-datatable ui-datatable-data</xsl:attribute>
					<xsl:attribute name="cellspacing">0</xsl:attribute>
					<xsl:attribute name="rules">all</xsl:attribute>
					<xsl:attribute name="border">1</xsl:attribute>
					<xsl:attribute name="style">1border-style:None;width:98%;border-collapse:collapse;</xsl:attribute>
					
					
					
					<xsl:apply-templates select="//Saldos"/>
				</xsl:element>
				<br/>
				<xsl:element name="table">
					<xsl:attribute name="border">1</xsl:attribute>
					<xsl:apply-templates select="//Total"/>
				</xsl:element>
			</xsl:element>
		</xsl:element>

	</xsl:template>

	
	<xsl:template match="Saldos">
		<xsl:element name="tr">
			<xsl:attribute name="style">background-color:#FFFDF2;</xsl:attribute>

			<!-- Celda Sector -->
			<xsl:element name="td">
				<xsl:attribute name="align">left</xsl:attribute>
				<xsl:attribute name="style">background-color:White;</xsl:attribute>
				
				
				
				<xsl:choose>
					<xsl:when test="position()=1">
						<xsl:call-template name="Span">
							<xsl:with-param name="Nombre"><xsl:value-of select="./DES_SECTOR/text()"/></xsl:with-param>
						</xsl:call-template>
						<xsl:value-of select="./DES_SECTOR"/>
					</xsl:when>
					<xsl:when test="position()!=1">
<!--						<xsl:value-of select="./DES_SECTOR"/>  -->
						<xsl:if test="./DES_SECTOR/text()!=preceding-sibling::Saldos[1]/DES_SECTOR/text()">
							<xsl:call-template name="Span">
								<xsl:with-param name="Nombre"><xsl:value-of select="./DES_SECTOR/text()"/></xsl:with-param>
							</xsl:call-template>
							<xsl:value-of select="./DES_SECTOR"/>
							
						</xsl:if>
						
						
					</xsl:when>
					
					
				</xsl:choose>
				
			</xsl:element>
			
			<!-- Celda Color -->
			<xsl:element name="td">
				<xsl:attribute name="class">ColunaSemMargin</xsl:attribute>
				<xsl:attribute name="align">center</xsl:attribute>
				<xsl:attribute name="style">color:#767676;width:7px;background:#<xsl:value-of select="./COD_COLOR"/> !important;</xsl:attribute>
			</xsl:element>
			
			<!-- Celda Divisa -->
			<xsl:element name="td">
				<xsl:value-of select="./DES_DIVISA"/>
			</xsl:element>

			<xsl:element name="td">
				<xsl:value-of select="./NUM_IMPORTE_DISP"/>
			</xsl:element>

			<xsl:element name="td">
				<xsl:value-of select="./NUM_IMPORTE_NODISP"/>
			</xsl:element>

			<xsl:element name="td">
				<xsl:value-of select="./NUM_IMPORTE"/>
			</xsl:element>

		</xsl:element>
	</xsl:template>

	<xsl:template match="Total">
		<xsl:element name="tr">
			<xsl:element name="td">
				<xsl:value-of select="./COD_COLOR"/>
			</xsl:element>
			<xsl:element name="td">
				<xsl:value-of select="./DES_DIVISA"/>
			</xsl:element>
			<xsl:element name="td">
				<xsl:value-of select="./NUM_IMPORTE_DISP"/>
			</xsl:element>
			<xsl:element name="td">
				<xsl:value-of select="./NUM_IMPORTE_NODISP"/>
			</xsl:element>
			<xsl:element name="td">
				<xsl:value-of select="./NUM_IMPORTE"/>
			</xsl:element>
		</xsl:element>
	</xsl:template>



	<xsl:template name="Span">
		<xsl:param name="Nombre"/>
		<xsl:variable name="Cantidad"><xsl:value-of select="count(following-sibling::Saldos[DES_SECTOR/text() = $Nombre])"/></xsl:variable>
		<xsl:attribute name="rowspan"><xsl:value-of select="$Cantidad"/></xsl:attribute>
	</xsl:template>
	
	

</xsl:stylesheet>
