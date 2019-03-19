#include<stdio.h>
#include<stdlib.h>
#include<string.h>
int Prule[64] =
{
    58,50,42,34,26,18,10,2,60,52,44,36,28,20,12,4,
    62,54,46,38,30,22,14,6,64,56,48,40,32,24,16,8,
    57,49,41,33,25,17,9,1,59,51,43,35,27,19,11,3,
    61,53,45,37,29,21,13,5,63,55,47,39,31,23,15,7
};

int RPrule[64] =
{
    40,8,48,16,56,24,64,32,39,7,47,15,55,23,63,31,
    38,6,46,14,54,22,62,30,37,5,45,13,53,21,61,29,
    36,4,44,12,52,20,60,28,35,3,43,11,51,19,59,27,
    34,2,42,10,50,18,58,26,33,1,41,9,49,17,57,25
};

const int Erule[48] =
{
    32,1,2,3,4,5,4,5,6,7,8,9,
    8,9,10,11,12,13,12,13,14,15,16,17,
    16,17,18,19,20,21,20,21,22,23,24,25,
    24,25,26,27,28,29,28,29,30,31,32,1
};

const int PCrule[32] =
{
    16,7,20,21,29,12,28,17,1,15,23,26,5,18,31,10,
    2,8,24,14,32,27,3,9,19,13,30,6,22,11,4,25
};

const int S[8][4][16] =
{
    // S1
    14,4,13,1,2,15,11,8,3,10,6,12,5,9,0,7,
    0,15,7,4,14,2,13,1,10,6,12,11,9,5,3,8,
    4,1,14,8,13,6,2,11,15,12,9,7,3,10,5,0,
    15,12,8,2,4,9,1,7,5,11,3,14,10,0,6,13,
    //S2
    15,1,8,14,6,11,3,4,9,7,2,13,12,0,5,10,
    3,13,4,7,15,2,8,14,12,0,1,10,6,9,11,5,
    0,14,7,11,10,4,13,1,5,8,12,6,9,3,2,15,
    13,8,10,1,3,15,4,2,11,6,7,12,0,5,14,9,
    //S3
    10,0,9,14,6,3,15,5,1,13,12,7,11,4,2,8,
    13,7,0,9,3,4,6,10,2,8,5,14,12,11,15,1,
    13,6,4,9,8,15,3,0,11,1,2,12,5,10,14,7,
    1,10,13,0,6,9,8,7,4,15,14,3,11,5,2,12,
    //S4
    7,13,14,3,0,6,9,10,1,2,8,5,11,12,4,15,
    13,8,11,5,6,15,0,3,4,7,2,12,1,10,14,9,
    10,6,9,0,12,11,7,13,15,1,3,14,5,2,8,4,
    3,15,0,6,10,1,13,8,9,4,5,11,12,7,2,14,
    //S5
    2,12,4,1,7,10,11,6,8,5,3,15,13,0,14,9,
    14,11,2,12,4,7,13,1,5,0,15,10,3,9,8,6,
    4,2,1,11,10,13,7,8,15,9,12,5,6,3,0,14,
    11,8,12,7,1,14,2,13,6,15,0,9,10,4,5,3,
    //S6
    12,1,10,15,9,2,6,8,0,13,3,4,14,7,5,11,
    10,15,4,2,7,12,9,5,6,1,13,14,0,11,3,8,
    9,14,15,5,2,8,12,3,7,0,4,10,1,13,11,6,
    4,3,2,12,9,5,15,10,11,14,1,7,6,0,8,13,
    //S7
    4,11,2,14,15,0,8,13,3,12,9,7,5,10,6,1,
    13,0,11,7,4,9,1,10,14,3,5,12,2,15,8,6,
    1,4,11,13,12,3,7,14,10,15,6,8,0,5,9,2,
    6,11,13,8,1,4,10,7,9,5,0,15,14,2,3,12,
    //S8
    13,2,8,4,6,15,11,1,10,9,3,14,5,0,12,7,
    1,15,13,8,10,3,7,4,12,5,6,11,0,14,9,2,
    7,11,4,1,9,12,14,2,0,6,10,13,15,3,5,8,
    2,1,14,7,4,10,8,13,15,12,9,0,3,5,6,11
};

const int KeyChange1Rule[56]=
{
    57,49,41,33,25,17,9,1,58,50,42,34,26,18,
    10,2,59,51,43,35,27,19,11,3,60,52,44,36,
    63,55,47,39,31,23,15,7,62,54,46,38,30,22,
    14,6,61,53,45,37,29,21,13,5,28,20,12,4
};

const int KeyChange2Rule[48]=
{
    14,17,11,24,1,5,3,28,15,6,21,10,
    23,19,12,4,26,8,16,7,27,20,13,2,
    41,52,31,37,47,55,30,40,51,45,33,48,
    44,49,39,56,34,53,46,42,50,36,29,32
};


void Reversal(int*p,int now,int l);
void IP(int* word,int*newWord,bool flag);
void Expend(const int*half,int*newHalf);
int* PChange(const int*p32); // 32->32
void SubInt(const int* a,int* subint,int index,int num);
void Show(const int* a,int num);
void KeyChange1(int *key,int* newkey);
void KeyChange2(int *key,int* newkey);
int* DES(int*,bool);
void ByteToBit(int *Out, const char*In, int bits);
void BitToByte(char *Out, const int *In, int bits);
int** SetKey();
int* Xor(int*e,int*k,int l);
void LMove(int *CD,int k,int);
void Combine(int* a,int* b,int* c,int l);
int* SChange(int *x);  //x 48位
int* FChange(int* Ri,int* ki);

int main()
{
    printf("请输入您的密文（8位）：");
    char word[8];
    scanf("%s",word);
    int wordbit[64];
    ByteToBit(wordbit, word, 64);
    int* pw = DES(wordbit,true);
    printf("二进制密文：");
    Show(pw,64);
    int* ppw = DES(wordbit,false);
    char w[8];
    BitToByte(w,ppw,64);
     printf("明文：");
    for(int i = 0;i<8;i++)
    printf("%c",w[i+8]);
    printf("\n\n\n");
    system("pause");
}

int* DES(int* word,bool flag)
{
    int** k = SetKey();    //秘钥
    int ip[64];
    IP(word,ip,true);
    int *Li = &ip[0], *Ri = &ip[32],LR[64],Tmp[32];

    if(flag == true){
        for(int i = 0; i<16; i++){
            memcpy(Tmp, Ri, 32);
            int expend[48];
            Expend(Ri,expend);   //结果存入expend
            int* xo = Xor(expend,k[i],48); //e xor k[i]
            int* sc = SChange(xo);      //S盒压缩变换
            int* pc  = PChange(sc);
            Ri = Xor(pc, Li,32);
            memcpy(Li, Tmp, 32);
        }
    }
    else
    {
        for(int i=15; i>=0; i--)
        {
            memcpy(Tmp, Li, 32);
            Li = FChange(Li, k[i]);
            Li = Xor(Li, Ri,32);
            memcpy(Ri, Tmp, 32);
        }
    }

    int* result = (int*)malloc(64*sizeof(int));
    IP(Li,result,false);
    return result;
}

int* FChange(int* Ri,int* ki)
{
    int expend[48];
    Expend(Ri,expend);   //结果存入expend
    int* xo  = Xor(expend,ki,48); //e xor k[i]
    int* sc  = SChange(xo);      //S盒压缩变换
    int* pc  = PChange(sc);
    return pc;
}

int* SChange(int *x)  //x 48位
{
    int* xc = (int*)malloc(32*sizeof(int));
    for(int i = 0,j=0; i<8; j+=6,i++)
    {
        int sub[6];
        SubInt(x,sub,j,6);
        //printf("sub");
        //Show(sub,6);
        LMove(sub,1,6);
        //printf("LmOve");
        //Show(sub,6);
        int row[2],col[4];
        SubInt(sub,col,0,4);
        SubInt(sub,row,4,2);
        Reversal(col,0,4);
         //printf("行");
        //Show(row,2);
         //printf("列");
        //Show(col,4);
        char p,q;
        BitToByte(&p,col,4);
        int c = p;
        //printf("整形列 %d\n",c);
        BitToByte(&q,row,2);
        int r = q;
        //printf("整形行 %d\n",r);
        int result = S[i][r][c];
        //printf("result  %d\n",result);
        int out[4];
        char re = result;
        ByteToBit(out,&re,4);
        Reversal(out,0,4);
        //Show(out,4);
        for(int m = i*4,n = 0; n<4; n++,m++)
            xc[m] = out[n];
    }
    return xc;
}

int* Xor(int*e,int*k,int l)
{
    int* x = (int*)malloc(l*sizeof(int));
    for(int i = 0; i<l; i++)
        x[i] = e[i]^k[i];
    return x;
}
void ByteToBit(int *Out, const char*In, int bits)
{
    for(int i=0; i<bits; i++)
        Out[i] = (In[i/8]>>(i%8)) & 1;
}
void BitToByte(char *Out, const int *In, int bits)
{
    memset(Out, 0, (bits+7)/8);
    for(int i=0; i<bits; i++)
        Out[i/8] |= In[i]<<(i%8);
}
int** SetKey()
{
    char key[]= {1,9,8,0,9,1,7,2};
    int keybit[64],key56[56],C[28],D[28];
    int** k = (int**)malloc(16*sizeof(int*));
    ByteToBit(keybit, key, 64);
    KeyChange1(keybit,key56);
    SubInt(key56,C,0,28);
    SubInt(key56,D,28,28);

    const int loop[] = {1,1,2,2,2,2,2,2,1,2,2,2,2,2,2,1};
    for(int i = 0; i<=16; i++)
    {
        LMove(C,loop[i],28);
        LMove(D,loop[i],28);
        int combine[56];
        Combine(C,D,combine,28);
        k[i] = (int*)malloc(48*sizeof(int));
        KeyChange2(combine,k[i]);
    }
    return k;
}
void Combine(int* a,int* b,int* c,int l)
{
    for(int i = 0; i<l; i++)
        c[i] = a[i];
    for(int i = l,j = 0; j<l; i++)
        c[i] = b[j++];
}
void LMove(int *CD,int k,int l)
{
    Reversal(CD,0,k);
    Reversal(CD,k,l-k);
    Reversal(CD,0,l);
}
//反转数组
void Reversal(int*p,int now,int l)
{
    int i,j;
    j = l+now-1;
    if(l/2)
        (++l)/=2;
    else
        l/=2;
    for(i = now; l--; i++,j--)
    {
        int temp;
        temp = p[i];
        p[i] = p[j];
        p[j] = temp;
    }
}
void IP(int* word,int*newWord,bool flag)  //  初始置换 TRUE   初始逆置换 FALSE
{
    int* realrule = (flag == true)?Prule:RPrule;
    for(int i = 0; i<64; i++)
        newWord[i] = word[realrule[i] - 1];
}
void Expend(const int*half,int*newHalf) // 32->48 拓展变换
{
    for(int i = 0; i<48; i++)
        newHalf[i] = half[Erule[i] - 1];
}
int* PChange(const int*p32) // 32->32
{
    int* tmp = (int*)malloc(32*sizeof(int));
    for(int i = 0; i<32; i++)
        tmp[i] = p32[PCrule[i] - 1];
    return tmp;
}
void SubInt(const int* a,int* subint,int index,int num)  //求子串 左 右
{
    for(int i = index,j = 0; j<num; i++)
        subint[j++] = a[i];
}
void Show(const int* a,int num)
{
    for(int i = 0; i<num; i++)
        printf("%d",a[i]);
    printf("\n");
}
void KeyChange1(int *key,int* newkey)
{
    for(int i = 0; i<56; i++)
        newkey[i] = key[KeyChange1Rule[i] - 1];
}
void KeyChange2(int *key,int* newkey)
{
    for(int i = 0; i<48; i++)
        newkey[i] = key[KeyChange2Rule[i] - 1];
}

