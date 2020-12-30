using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// 树形节点
/// </summary>
public class TreeNode
{
    /// <summary>
    /// id
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// 编号
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// 标签
    /// </summary>
    public string Label { get; set; }

    /// <summary>
    /// 是否禁用
    /// </summary>
    public bool Disabled { get; set; }

    /// <summary>
    /// 是否选择
    /// </summary>
    public bool Selected { get; set; }

    /// <summary>
    /// 图标
    /// </summary>
    public string Icon { get; set; }

    /// <summary>
    /// 是否是叶子节点
    /// </summary>
    public bool IsLeaf { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int? Sort { get; set; }

    /// <summary>
    /// 链接地址
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// 是否全选
    /// </summary>
    public bool IsIndeterminate { get; set; }

    /// <summary>
    /// 子节点
    /// </summary>
    public List<TreeNode> Children { get; set; }

    /// <summary>
    /// 无参构造函数
    /// </summary>
    public TreeNode()
        : this(string.Empty)
    { }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="_label"></param>
    /// <param name="_children"></param>
    /// <param name="_disabled"></param>
    /// <param name="_isLeaf"></param>
    public TreeNode(string _label, List<TreeNode> _children = null, bool _disabled = false, bool _isLeaf = false)
    {
        Label = _label;
        Children = _children ?? new List<TreeNode>();
        Disabled = _disabled;
        IsLeaf = _isLeaf;
    }
}
