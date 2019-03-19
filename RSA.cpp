#include<stdio.h>
#include <time.h>
#include<stdlib.h>
int quickModPow(long long a, long long b, int c);  // ���� a^b mod c
int niyuan(int a, int fain);//����Ԫ 
int getPrime(int n,int time);//  Rabin-miller �õ�һ������ ��Χ��2��n��, time Ϊ����������� 
int main()
{
	const int N = 100000100; //��̬����[2��N]������
	int p = getPrime(N,(unsigned)time(NULL));
	int q = getPrime(N,(unsigned)time(NULL)*9);
	int n = p*q,fain = (p-1)*(q-1);
	int e[10] = {3,5,7,11,13,17,19,23,31,97};
	int i = 0;
	for(i = 0;i<10;i++)
	{
		if(fain%e[i]==0)
		 continue;
		else
		 break;
	 }
	//��Կ {e,n}
	printf("��Կ e: %d n: %d\n",e[i],n);
	int m;
	printf("������������m��mӦ�� [1,%d]��Χ��\n",fain);
	scanf("%d",&m);
	int d = niyuan(e[i],fain);
	printf("˽Կ d: %d p: %d q: %d\n",d,p,q);
	
	int c = quickModPow(m,e[i],n);
	printf("����C: %d\n",c);	
	int M = quickModPow(c,d,n);
	printf("����M��%d\n",M);
	
	system("pause");
	
	return 0;
}
//���� 
int quickModPow(long long a, long long b, int c) {
    int res = 1;
    while(b > 0) {
        if(b & 1) {
            res = (res * a) % c;
        }
        b = b >> 1;
        a = (a * a) % c; /* Same deal here */
    }
    return res;
}
//�˷���Ԫ 
int niyuan(int a, int fain) 
{
	int tmp = fain;
	int b[100] = {0},i = 2;
	b[1] = 1;
    while(fain%a!=0){
    	b[i] = (-1)*b[i-1]*(int)(fain/a)+b[i-2];
    	i++;
    	int temp = a;
    	a = fain%a;
    	fain = temp;
	}
	if(b[i-1]<0)
		b[i-1]+=tmp;
	return b[i-1];
}
//�ſ˱� 
int jacobi(int a, int n) {
    int twos, temp;
    int mult = 1;
    while(a > 1 && a != n) {
        a = a % n;
        if(a <= 1 || a == n) break;
        twos = 0;
        while(a % 2 == 0 && ++twos) a /= 2; 
        if(twos > 0 && twos % 2 == 1) mult *= (n % 8 == 1 || n % 8 == 7) * 2 - 1;
        if(a <= 1 || a == n) break;
        if(n % 4 != 1 && a % 4 != 1) mult *= -1;
        temp = a;
        a = n;
        n = temp;
    }
    if(a == 0) return 0;
    else if(a == 1) return mult;
    else return 0;
}
// 
int checkPrime(int a, int n) {
    int x = jacobi(a, n);
    if(x == -1) x = n - 1;
    return x != 0 && quickModPow(a, (n - 1)/2, n) == x;
}
//Rabin-miller �����ж� 
int probablePrime(int n, int k) {
	srand((unsigned)time(NULL));
    if(n == 2) return 1;
    else if(n % 2 == 0 || n == 1) return 0;
    while(k-- > 0) {
        if(!checkPrime(rand() % (n - 2) + 2, n)) return 0;
    }
    return 1;
}

int getPrime(int n,int time) {
	srand(time);
    int prime = rand() % n;
    n += n % 2;
    prime += 1 - prime % 2;
    while(1) {
        if(probablePrime(prime, 10)) return prime;
        prime = (prime + 2) % n;
    }
}
        
