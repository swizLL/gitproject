#include <iostream>
#include <vector>
#include "math.h"
using namespace std;
double qiua(vector<double> v1,double pj)
{
    double can=0,sum1=0,ab1=0;//定义了残差，残差的平方和,A类不确定度
    vector<double> v2;//定义了储存残差的vector
    auto beg=v1.begin();
    auto en=v1.end();
    auto n=v1.size();
    for(auto c=beg;c!=en;++c)
    {
	//求出残差
	can=*c-pj;
	v2.push_back(can);
    }
    for(auto d=v2.begin();d!=v2.end();++d)
    {
	sum1+=pow(*d,2);
    }
    ab1=sqrt(sum1/(n-1));
    return ab1;
}
double qiub(double del)
{
    double bb1=del/sqrt(3);
    return bb1;
}
int main()
{
    //记录输入数据的个数
    int n=0;
    vector<double> v;
    double a=0,sum=0,ap=0,delta=0,ab=0,bb=0,zb=0;//分别定义要处理的数据，数据之和,平均值，最小分度值，a类不确定度，b类不确定度，总不确定度
    cout<<"请输入您要处理的数据(输入句号结束)："<<endl;
    while(cin>>a&&a!='.')
    {
	v.push_back(a);
	++n;
	sum+=a;
    }
    cin.sync();
    cin.clear();
    cin.ignore();
    if(n==0)
    {
	cout<<"您没有输入数据!"<<endl;
	return 0;
    }
    else
    {
	ap=sum/n;
    }
    cout<<"请输入delta值："<<endl;
    cin>>delta;
    ab=qiua(v,ap);
    bb=qiub(delta);
    zb=sqrt(pow(ab,2)+pow(bb,2));
    cout<<"平均值："<<ap<<endl;
    cout<<"A类不确定度："<<ab<<endl;
    cout<<"B类不确定度："<<bb<<endl;
    cout<<"总不确定度："<<zb<<endl;
}
