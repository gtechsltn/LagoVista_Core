﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using LagoVista.Core.Models.UIMetaData;

namespace LagoVista.Core.Attributes
{
    public enum FieldTypes
    {
        RowId,
        Picker,
        Hidden,
        Text,
        Key,
        MultiLineText,
        Integer,
        Decimal,
        Date,
        Time,
        LinkButton,
        DateTime,
        Phone,
        Password,
        Email,
        Bool,
        ChildList,
        ChildItem,
        ChildView,
        NodeScript,
        JsonDateTime,
        NameSpace,
        CheckBox,
        OptionsList,
        GeoLocation,
        EntityHeaderPicker,
        Byte,
        Secret,
        Icon,
        Color,
        ChildListInline
    }

    public enum NamespaceTypes
    {
        None,
        Organization,
        Location,
        DeviceGroup
    }

    [AttributeUsage( AttributeTargets.Property)]
    public class FormFieldAttribute : Attribute
    {
        Type _resourceType;
        Type _enumType;

        private string _labelDisplayResource;
        private string _columnHeaderDisplayResource;
        private string _requiredMessageResources;
        private string _regExValidation;
        private string _regExValidationMessageResource;
        private bool _isRequired;
        private bool _isUserEditable;
        private bool _isMarkDown;
        private string _helpResource;
        private string _waterMark;
        private int? _minLength;
        private int? _maxLength;
        private string _compareTo;
        private string _pickerFor;
        private string _compareToMsgResource;
        private string _namespaceUniqueMessageResource;
        private string _childListDisplayMember;
        private FieldTypes _fieldType;
        private string _pickerType;
        private NamespaceTypes _namespaceType;
    
        public FormFieldAttribute(String LabelResource = "", 
                                  String ColHeaderResource = "",
                                  String ValidationRegEx = "",
                                  String ReqMessageResource = "",
                                  String HelpResource = "",
                                  String WaterMark = "",
                                  String CompareTo = "",
                                  String CompareToMsgResource = "",
                                  String RegExValidationMessageResource = "",
                                  String PickerType = "",
                                  String PickerFor = "",
                                  String RegExMessage = "",
                                  String NamespaceUniqueMessageResource = "",
                                  String ChildListDisplayMember = "name",
                                  NamespaceTypes NamespaceType = NamespaceTypes.None,
                                  int MinLength = -1,
                                  int MaxLength = -1,
                                  bool IsRequired = false,
                                  bool IsUserEditable = true,
                                  bool IsMarkDown = false,
                                  FieldTypes FieldType = FieldTypes.Text,
                                  Type ResourceType = null,
                                  Type EnumType = null)
        {
            _labelDisplayResource = LabelResource;
            _columnHeaderDisplayResource = ColHeaderResource;
            _regExValidation = ValidationRegEx;
            _requiredMessageResources = ReqMessageResource;
            _regExValidationMessageResource = RegExValidationMessageResource;
            _isRequired = IsRequired;
            _fieldType = FieldType;
            _isUserEditable = IsUserEditable;
            _helpResource = HelpResource;
            _resourceType = ResourceType;
            _pickerType = PickerType;
            _pickerFor = PickerFor;
            _isMarkDown = IsMarkDown;
            _waterMark = WaterMark;
            _minLength = MinLength < 0 ? (int?)null : MinLength;
            _maxLength = MaxLength < 0 ? (int?)null : MaxLength;
            _compareTo = CompareTo;
            _compareToMsgResource = CompareToMsgResource;
            _namespaceUniqueMessageResource = NamespaceUniqueMessageResource;
            _namespaceType = NamespaceType;
            _enumType = EnumType;
            _childListDisplayMember = ChildListDisplayMember;
        }

        public String PickerFor { get { return _pickerFor; } }
        public String PickerType { get { return _pickerType; } }
        public String HelpResource { get { return _helpResource; } }
        public String LabelDisplayResource { get { return _labelDisplayResource; } }
        public String RequiredMessageResource { get {return _requiredMessageResources; } }
        public String NamespaceUniqueMessageResource { get { return _namespaceUniqueMessageResource; } }
        public String RegExValidation { get { return _regExValidation; } }
        public String RegExValidationMessageResource { get { return _regExValidationMessageResource; } }
        public bool IsRequired { get { return _isRequired; } }
        public bool IsMarkDown { get { return _isMarkDown; } }
        public bool IsUserEditable { get { return _isUserEditable; } }
        public FieldTypes FieldType { get { return _fieldType; } }
        public Type ResourceType { get { return _resourceType; } }
        public string  WaterMark { get { return _waterMark; } }
        public string CompareTo { get { return _compareTo; } }
        public string CompareToMsgResource { get { return _compareToMsgResource; } }
        public int? MinLength { get { return _minLength; } }
        public int? MaxLength { get { return _maxLength; } }
        public NamespaceTypes NamespaceType { get { return _namespaceType; } }
        public Type EnumType { get { return _enumType; } }
        public string ChildListDisplayMember { get { return _childListDisplayMember; } }
    }

    public class SelectListItem
    {
        public String Key { get; set; }
        public String Value { get; set; }
    }
}
