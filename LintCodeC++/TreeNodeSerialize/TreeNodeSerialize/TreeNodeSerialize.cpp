// TreeNodeSerialize.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <stdio.h>
#include <iostream>
#include <string>
#include <vector>
#include <queue>
using namespace std;
class TreeNode {
public:
	int val;
	TreeNode *left, *right;
	TreeNode(int val) {
		this->val = val;
		this->left = this->right = NULL;

	}
};
string serialize(TreeNode * root) 
{
	// write your code here
	string temp;//定义一个临时的字符串
	if (root == NULL)
	{
		return temp;
	}
	char str[10];
	queue<TreeNode*> buf;
	buf.push(root);//把第一个根节点先存进队列
	while (!buf.empty())//当队列不为空时，则继续读取
	{
		TreeNode *front = buf.front();
		if (front != NULL)//父节点不为空时，读取数据
		{
			sprintf_s(str, "%d", front->val);
			temp += str;//将字符加入到字符串中
			buf.push(front->left);
			buf.push(front->right);
		}
		else
		{
			temp += "#";
		}
		temp += " ";
		buf.pop();//删除最后一个节点
	}

	int end = temp.length() - 1;
	while (temp[end] == '#' || temp[end] == ' ')
	{
		end--;
	}
	return temp.substr(0, end + 1);
}

TreeNode * deserialize(string &data) 
{
	// write your code here
	int n = data.length();
	if (n < 1)
	{
		return NULL;
	}
	vector<int> pos;
	pos.push_back(-1);
	for (int i = 0; i < n; ++i)
	{
		if (data[i] == ' ')//每个字符后面都跟有一个' '
		{
			pos.push_back(i);
		}
	}
	if (pos.size() == 1)
	{
		return new TreeNode(atoi(data.c_str()));//c_str()函数返回一个指向正规C字符串的指针常量, 内容与本string串相同. 这是为了与c语言兼容，在c语言中没有string类型，故必须通过string类对象的成员函数c_str()把string 对象转换成c中的字符串样式。
	}
	pos.push_back(n);
	vector<TreeNode*> treeNode;
	for (int i = 0; i < pos.size()-1; ++i)
	{
		string vall = data.substr(pos[i] + 1, pos[i+1]-pos[i]-1);
		if (vall == "#")
		{
			treeNode.push_back(NULL);
		}
		else {
			TreeNode *p = new TreeNode(atoi(vall.c_str()));
			treeNode.push_back(p);
		}
	}
	int m = treeNode.size();
	int p = 1;
	int q = 0;
	while (p < m)
	{
		if (treeNode[q] == NULL)
		{
			q++;
		}
		else
		{
			treeNode[q]->left = treeNode[p];
			treeNode[q]->right = treeNode[p + 1];
			p += 2;
			q++;
		}
	}

	return treeNode[0];
}
int main()
{
	string s = { 3,9,20,'#','#',15,7 };
	deserialize(s);
	serialize(deserialize(s));
}