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
					<xsl:attribute name="style">border-style:None;width:98%;border-collapse:collapse;</xsl:attribute>
					<tbody>			
						<tr>				
							<th scope="col" style="font-size:11px;">Sector</th>
							<th class="ColunaSemMargin" scope="col" style="font-size:10px;width:7px;"></th>
							<th scope="col" style="font-size:11px;">Divisa</th>
							<th scope="col" style="font-size:11px;">Valor Disponible</th>
							<th scope="col" style="font-size:11px;">Valor No Disponible</th>
							<th scope="col" style="font-size:11px;">Suma</th>
						</tr>					
					
					
					<xsl:apply-templates select="//Saldos"/>
					
					</tbody>
				</xsl:element>

				<br/>

				<xsl:element name="table">
					<xsl:attribute name="style">width: 98%; margin-left: 3px;</xsl:attribute>
					
					<xsl:element name="tbody">
						<xsl:element name="tr">
							<xsl:element name="td">
								<xsl:attribute name="style">width: 38%</xsl:attribute>
							</xsl:element>
						
							<xsl:element name="td">
								<xsl:attribute name="style">text-align: right; margin-left: 5px;</xsl:attribute>
								<xsl:element name="div">

									<xsl:element name="table">
										<xsl:attribute name="class">ui-datatable ui-datatable-data</xsl:attribute>
										<xsl:attribute name="cellspacing">0</xsl:attribute>
										<xsl:attribute name="rules">all</xsl:attribute>
										<xsl:attribute name="border">1</xsl:attribute>
										<xsl:attribute name="style">border-style:None;border-collapse:collapse;</xsl:attribute>
										
										<xsl:element name="tbody">
											<xsl:element name="tr">
												<xsl:element name="th">
													<xsl:attribute name="rowspan">
														<xsl:value-of select="count(//Total) + 1"/>
													</xsl:attribute>
													<xsl:attribute name="scope">col</xsl:attribute>
													<xsl:attribute name="style">font-size:11px;width:101px;</xsl:attribute>Totales												
												</xsl:element>
												<th class="ColunaSemMargin" scope="col" style="font-size:10px;width:7px;"></th>
												<th scope="col" style="width:200px;">Divisa</th><th scope="col" style="width:150px;">Valor Disponible</th>
												<th scope="col" style="width:150px;">Valor No Disponible</th><th scope="col" style="width:150px;">Suma</th>
											</xsl:element>
	
											<xsl:apply-templates select="//Total"/>

										</xsl:element>
									</xsl:element>
								</xsl:element>
							</xsl:element>
						</xsl:element>
					</xsl:element>
				</xsl:element>
			</xsl:element>
		</xsl:element>

	</xsl:template>

	
	<xsl:template match="Saldos">
		<xsl:element name="tr">
			<xsl:attribute name="style">background-color:#FFFDF2;</xsl:attribute>

			<!-- Celda Sector -->
			<xsl:call-template name="Sector">
				<xsl:with-param name="NombreSec"><xsl:value-of select="./DES_SECTOR/text()"/></xsl:with-param>
			</xsl:call-template>
			
			<!-- Celda Color -->
			<xsl:element name="td">
				<xsl:attribute name="class">ColunaSemMargin</xsl:attribute>
				<xsl:attribute name="align">center</xsl:attribute>
				<xsl:attribute name="style">color:#767676;width:7px;background:#<xsl:value-of select="./COD_COLOR"/> !important;</xsl:attribute>
			</xsl:element>
			
			<!-- Celda Divisa -->
			<xsl:element name="td">
				<xsl:attribute name="align">left</xsl:attribute>
				<xsl:attribute name="style">background-color:White;width:200px;</xsl:attribute>
				<xsl:value-of select="./DES_DIVISA"/>
			</xsl:element>

			<xsl:element name="td">
				<xsl:attribute name="align">right</xsl:attribute>
				<xsl:attribute name="style">background-color:White;width:150px;</xsl:attribute>

				
				<xsl:choose>
					<xsl:when test="./NUM_IMPORTE_DISP">
					<xsl:value-of select="format-number(./NUM_IMPORTE_DISP, '###,###,###,###,##0.00')"/>
					</xsl:when>
					<xsl:otherwise>0,00</xsl:otherwise>
				</xsl:choose>				

			</xsl:element>

			<xsl:element name="td">
				<xsl:attribute name="align">right</xsl:attribute>
				<xsl:attribute name="style">background-color:White;width:150px;</xsl:attribute>

				<xsl:choose>
					<xsl:when test="./NUM_IMPORTE_NODISP">
					<xsl:value-of select="format-number(./NUM_IMPORTE_NODISP, '###,###,###,###,##0.00')"/>
					</xsl:when>
					<xsl:otherwise>0,00</xsl:otherwise>
				</xsl:choose>				

			</xsl:element>

			<xsl:element name="td">
				<xsl:attribute name="align">right</xsl:attribute>
				<xsl:attribute name="style">background-color:White;width:150px;</xsl:attribute>
				
				<xsl:choose>
					<xsl:when test="./NUM_IMPORTE">
					<xsl:value-of select="format-number(./NUM_IMPORTE, '###,###,###,###,##0.00')"/>
					</xsl:when>
					<xsl:otherwise>0,00</xsl:otherwise>
				</xsl:choose>				
				
			</xsl:element>

		</xsl:element>
	</xsl:template>

	<xsl:template match="Total">
		<xsl:element name="tr">
			
			<xsl:element name="td">
				<xsl:attribute name="style">color:#767676;width:7px;background:#<xsl:value-of select="./COD_COLOR"/> !important;</xsl:attribute>
			</xsl:element>
			<xsl:element name="td">
				<xsl:value-of select="./DES_DIVISA"/>
			</xsl:element>

			<xsl:element name="td">
				<xsl:choose>
					<xsl:when test="./NUM_IMPORTE_DISP">
					<xsl:value-of select="format-number(./NUM_IMPORTE_DISP, '###,###,###,###,##0.00')"/>
					</xsl:when>
					<xsl:otherwise>0,00</xsl:otherwise>
				</xsl:choose>				
			</xsl:element>
			
			<xsl:element name="td">
				<xsl:choose>
					<xsl:when test="./NUM_IMPORTE_NODISP">
					<xsl:value-of select="format-number(./NUM_IMPORTE_NODISP, '###,###,###,###,##0.00')"/>
					</xsl:when>
					<xsl:otherwise>0,00</xsl:otherwise>
				</xsl:choose>				
			</xsl:element>
			
			<xsl:element name="td">
				<xsl:choose>
					<xsl:when test="./NUM_IMPORTE">
					<xsl:value-of select="format-number(./NUM_IMPORTE, '###,###,###,###,##0.00')"/>
					</xsl:when>
					<xsl:otherwise>0,00</xsl:otherwise>
				</xsl:choose>				
			</xsl:element>
			
		</xsl:element>
	</xsl:template>

	<xsl:template name="Sector">
		<xsl:param name="NombreSec"/>
	
				<xsl:choose>
					<xsl:when test="position()=1">
						<xsl:call-template name="Sector2">
							<xsl:with-param name="NombreSector"><xsl:value-of select="$NombreSec"/></xsl:with-param>
						</xsl:call-template>
					</xsl:when>

					<xsl:when test="position()!=1">
						<xsl:if test="$NombreSec!=preceding-sibling::Saldos[1]/DES_SECTOR/text()">
							<xsl:call-template name="Sector2">
								<xsl:with-param name="NombreSector"><xsl:value-of select="$NombreSec"/></xsl:with-param>
							</xsl:call-template>
						</xsl:if>
					</xsl:when>
					
					
				</xsl:choose>
	
	
	</xsl:template>



	<xsl:template name="Sector2">
		<xsl:param name="NombreSector"/>

		<xsl:element name="td">
			<xsl:attribute name="align">left</xsl:attribute>
			<xsl:attribute name="style">background-color:White;</xsl:attribute>
			<xsl:call-template name="Span">
				<xsl:with-param name="Nombre"><xsl:value-of select="$NombreSector"/></xsl:with-param>
			</xsl:call-template>
			<xsl:value-of select="$NombreSector"/>
		</xsl:element>
	</xsl:template>



	<xsl:template name="Span">
		<xsl:param name="Nombre"/>
		<xsl:variable name="Cantidad"><xsl:value-of select="count(following-sibling::Saldos[DES_SECTOR/text() = $Nombre])"/></xsl:variable>
		<xsl:attribute name="rowspan"><xsl:value-of select="$Cantidad + 1"/></xsl:attribute>
	</xsl:template>
	

	

</xsl:stylesheet>
