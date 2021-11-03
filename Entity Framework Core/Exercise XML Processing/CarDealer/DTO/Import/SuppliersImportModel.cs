﻿using System.Xml.Serialization;

namespace CarDealer.DTO.Import
{
    [XmlType("Supplier")]

    public class SuppliersImportModel
    {
     
            [XmlElement("name")]
            public string Name { get; set; }

            [XmlElement("isImporter")]
            public bool IsImporter { get; set; }

        }
}