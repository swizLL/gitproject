// MinStack.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>
using namespace std;
class Minstack
{
public:
	struct Node {
		int value;
		Node *next;
	};
	Node *head;//栈底
	Node *p;//栈顶
	int length;
	Minstack()
	{
		head = NULL;
		length = 0;
	}
	void push(int number)
	{
		Node *q = new Node;
		q->value = number;
		if (head = NULL)
		{
			q->next = head;
			head = q;
			p = q;
		}
		else
		{
			q->next = p;
			p = q;
		}
		length++;
	}
	int pop()
	{
		if (length <= 0)
		{
			abort();
		}
		Node *q;
		int v;
		q = p;
		v = p->value;
		p = p->next;
		delete(q);
		length--;
		return v;
	}
	int min()
	{
		int minVal = p->value;
		Node *q;
		q = p->next;
		if (length <= 0)
		{
			abort();
		}
		if (length == 1)
			return minVal;
		for (int i = 0; i < length-1; ++i)
		{
			if (q->value >= minVal)
			{
				q = q->next;
			}
			else
			{
				minVal = q->value;
				q = q->next;
			}
		}
		return minVal;
	}
};

int main()
{
	Minstack s;
	s.push(1);
	cout << s. min() << endl;
	s.push(2);
	cout << s.min()<<endl;
	s.push(3);
	cout << s.min()<<endl;
	return 0;
}
