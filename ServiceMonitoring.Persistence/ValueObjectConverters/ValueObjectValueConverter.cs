using Microservice.Framework.Common;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceMonitoring.Persistence.ValueObjectConverters
{
    public class ValueObjectValueConverter<TValueObjectType, TValueObjectTypes> : ValueConverter<TValueObjectType, string>
        where TValueObjectType : XmlValueObject
        where TValueObjectTypes : XmlValueObjectLookup<TValueObjectType, TValueObjectTypes>
    {
        public ValueObjectValueConverter(ConverterMappingHints mappingHints = null)
        : base(
            valueObject => valueObject.Code,
            value => XmlValueObjectLookup<TValueObjectType, TValueObjectTypes>.Of().AllowedItems.FirstOrDefault(s => s.Code == value),
            mappingHints
        )
        { }
    }
}
