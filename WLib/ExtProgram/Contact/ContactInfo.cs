namespace WLib.ExtProgram.Contact
{
    /// <summary>
    /// 联系信息
    /// </summary>
    public class ContactInfo
    {
        /// <summary>
        /// 联系方式类型
        /// </summary>
        public EContactType ContactType { get; set; }
        /// <summary>
        /// 联系方式名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 联系信息内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 联系信息
        /// </summary>
        public ContactInfo() { }
        /// <summary>
        /// 联系信息
        /// </summary>
        /// <param name="contactType">联系方式类型</param>
        /// <param name="name">联系方式名称</param>
        /// <param name="content">联系信息内容</param>
        public ContactInfo(EContactType contactType, string name, string content)
        {
            this.ContactType = contactType;
            this.Name = name;
            this.Content = content;
        }
    }
}
