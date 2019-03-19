#include <stdio.h>
#include <time.h>
#include <stdlib.h>
#include <conio.h>
#include <string.h>
#include <windows.h>      // ���ٻ�������ʱ
int map[4][4] = { 0 };
void Print();
void Add(int);
void Move();
void MoveUp();
void MoveDown();
void MoveLeft();
void MoveRight();
int Judge();
void Reset();
void Start();
void Face();
void Help();
void List();
int score = 0;     //����
int theBest;       //��߼�¼
int main()
{
	Face();
	return 0;
}
void Face()
{
    printf("\n\n\n\n");
	printf("\t\t\t\t\t2048\n\n");
    printf("\t\t\t������������������������������������\n");
	printf("\t\t\t��             1.Start            ��\n");
	printf("\t\t\t������������������������������������\n");
	printf("\t\t\t��             2.Rank             ��\n");
	printf("\t\t\t������������������������������������\n");
	printf("\t\t\t��             3.Help             ��\n");
	printf("\t\t\t������������������������������������\n");
	printf("\t\t\t��             0.Exit             ��\n");
	printf("\t\t\t������������������������������������\n");
	int n;
	scanf("%d",&n);
	system("cls");
	switch(n)
	{
	    case 1:Start();break;
	    case 2:List();
	      break;
        case 3: Help();
        break;
        case 0: exit(0);
	}
}
void Help()
{
	printf("\n\n��ʼʱ��������������������֣����ֵ����ֽ�����Ϊ2��4,��ҿ���ѡ�����������ĸ������������ڵ����ֳ���λ�ƻ�ϲ�����Ϊ��Ч�ƶ�,���ѡ��ķ�����������ͬ��������ϲ���ÿ����Ч�ƶ�����ͬʱ�ϲ����������������ϲ�.�ϲ����õ�����������������Ӽ�Ϊ�ò�����Ч�÷�,���ѡ��ķ����л���ǰ���пո������λ��,ÿ��Ч�ƶ�һ�������̵Ŀ�λ(�����ִ�)�������һ������(��Ȼ����Ϊ2��4).���̱������������޷�������Ч�ƶ����и�����Ϸ����;�����ϳ���2048����ʤ����Ϸ����\n");
    printf("�س�������һ�㣡\n");
    system("pause");
    system("cls");
    Face();
}
void Start()
{
	Add(0);
	while (1)
	{

		if (!Judge())
		{
		    if(Judge()==2)
		      printf("\t\t\tYOU WIN!!");
			Print();
			Move();
			COORD pos = {0, 0};
			SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE), pos);   //����������������˸
		}
		else
			break;
	}

	Print();
	char flag;
	printf("\t\t\t��Ϸ����!���յ÷�Ϊ%d \n", score);
	Reset();
	printf("\t\t\t�Ƿ�Ҫ����һ��?[0/1]");
	scanf("%d", &flag);
	system("cls");
	switch (flag)
	{
	case 0:
		Face(); break;
	case 1:
         Start();break;
	}
}

void Print()
{
	int i,j;
	FILE *fp;
	if ((fp = fopen("score.txt", "r")) == NULL)
	{
		printf("��߼�¼�ļ�û���ҵ���\n");
		exit(0);
	}
	fscanf(fp, "%d", &theBest);
	fclose(fp);
	printf("\n\n\n\n");
	printf("\t\t\t  ��߷�Ϊ:%d\t   ��ǰ����Ϊ:%d\n",theBest,score);
	printf("\t\t\t-----------------------------------\n\n");
	printf("\t\t\t���������Щ������Щ������Щ�������\n");
	for(i = 0; i < 4; i++)
	{
		printf("\t\t\t��");
		for(j = 0; j < 4; j++)
		{
			if(map[i][j] != 0)
					printf("%-6d��", map[i][j]);
			else
				printf("      ��");
		}
		printf("\n");
		if(i < 3)
			printf("\t\t\t���������੤�����੤�����੤������\n");
	}
	printf("\t\t\t���������ة������ة������ة�������\n");
}
//������ֵ�ĺ���
void Add(int dir)
{
	int row, col, num;
	int flag;
	srand((unsigned int)time(NULL));
	flag = rand() % 2;
	if (flag == 0)
		num = 2;
	else
		num = 4;
	row = rand() % 4;
	col = rand() % 4;
	switch (dir)
	{
	case 0:
	{
			 map[row][col] = num;
			  row = rand() % 4;
			  col = rand() % 4;
			  flag = rand() % 2;
	         if (flag == 0)
		       num = 2;
	         else
		       num = 4;
		 if (map[row][col] == 0)
			 map[row][col] = num;

	}break;
	case 1:
	{
			  if (map[row][col] == 0)
				  map[row][col] = num;
			  else
			  {
				  row = rand() % 4;
				  col = rand() % 4;
				  if (map[row][col] == 0)
				  {
					  map[row][col] = num;
				  }
				  else
				   for (col = 0; col < 4; col++)
					  if (map[3][col] == 0)
					  {
						  map[3][col] = num;
						  break;
					  }
			  }
	}
		break;
	case 2:
	{
			  if (map[row][col] == 0)
				  map[row][col] = num;
			  else
			  {
				  row = rand() % 4;
				  col = rand() % 4;
				  if (map[row][col] == 0)
				  {
					  map[row][col] = num;
				  }
				  else
				  for (col = 0; col < 4; col++)
				  if (map[0][col] == 0)
				  {
					  map[0][col] = num;
					  break;
				  }
			  }
	}
		break;
	case 3:
	{
			  if (map[row][col] == 0)
				  map[row][col] = num;
			  else
			  {
				  row = rand() % 4;
				  col = rand() % 4;
				  if (map[row][col] == 0)
				  {
					  map[row][col] = num;
				  }
			   else
				  for (row = 0; row < 4; row++)
				  if (map[row][3] == 0)
				  {
					  map[row][3] = num;
					  break;
				  }
			  }
	}
		break;
	case 4:
	{
	  if (map[row][col] == 0)
		map[row][col] = num;
	else
	{
		row = rand() % 4;
		col = rand() % 4;
		if (map[row][col] == 0)
		{
			map[row][col] = num;
		}
		else
		for (row = 0; row < 4; row++)
		if (map[row][0] == 0)
		{
			map[row][0] = num;
			break;
		}
	}
	}
		break;
	}
}
//���ƺ��� ���ط���
void Move()
{
	char ch;
	ch = getch();
	switch (ch)
	{
	case 72: case 'w':
		MoveUp();
		break;
	case 80: case 's':
		MoveDown();
		break;
	case 75: case 'a':
		MoveLeft();
		break;
	case 77: case 'd':
		MoveRight();
		break;
	}
}
void MoveUp()
{
	int row, col;
	int k = 0,g = 0;       //������
	for (col = 0; col < 4; col++)
	{
		int n = 4;
		while (n--){
			for (row = 0; row < 3; row++)
			{
				if (map[row][col] == 0&&map[row + 1][col] != 0)
				{
				    k++;
					map[row][col] = map[row + 1][col];
					map[row + 1][col] = 0;
				}
			}

		}
		int i;
			for (row = 0; row < 3; row++)
			{
				if (map[row][col] == map[row + 1][col]&&map[row][col])
				{
				    g++;
					score += map[row][col];
					map[row][col] *= 2;
					for (i = row+1; i < 3; i++)
					{
						map[i][col] = map[i + 1][col];
						map[i + 1][col] = 0;
					}

				}
			}

	}
	if(k||g)
      Add(1);
}

void MoveDown()
{
	int row, col, i,k = 0,g = 0;
	for (col = 0; col < 4; col++)
	{
		int n = 4;
		while (n--)
		{
			for (row = 3; row > 0; row--)
			{
				if (map[row][col] == 0&&map[row - 1][col] != 0)
				{
				    k++;
					map[row][col] = map[row - 1][col];
					map[row - 1][col] = 0;
				}
			}
		}
		for (row = 3; row > 0; row--)
		{
			if (map[row][col] == map[row - 1][col]&&map[row][col])
			{
				score += map[row][col];
				map[row][col] *= 2;
				for (i = row - 1; i > 0; i--)
				{
				    g++;
					map[i][col] = map[i - 1][col];
					map[i - 1][col] = 0;
				}

			}
		}
	}
	if(k||g)
	  Add(2);
}

void MoveLeft()
{
	int row, col, i,k,g;
	k = g = 0;
	for (row = 0; row < 4; row++)
	{
		int n = 4;
		while (n--)
		{
			for (col = 0; col < 3; col++)
			{
				if (map[row][col] == 0&&map[row][col + 1]!=0)
				{
				    k++;
					map[row][col] = map[row][col + 1];
					map[row][col + 1] = 0;
				}
			}
		}
		for (col = 0; col < 3; col++)
		{
			if (map[row][col] == map[row][col + 1]&&map[row][col])
			{
			    g++;
				score += map[row][col];
				  map[row][col] *= 2;
				for (i = col + 1; i < 3; i++)
				{
					map[row][i] = map[row][i + 1];
					map[row][i + 1] = 0;
				}
			}
		}
	}
	if(k||g)
	  Add(3);
}

void MoveRight()
{
	int row, col, i,k = 0,g = 0;
	for (row = 0; row < 4; row++)
	{
		int n = 4;
		while (n--)
		{
			for (col = 3; col > 0; col--)
			{
				if (map[row][col] == 0&&map[row][col - 1]!=0)
				{
				    k++;
					map[row][col] = map[row][col - 1];
					map[row][col - 1] = 0;
				}
			}
		}
		for (col = 3; col > 0; col--)
		{
			if (map[row][col] == map[row][col - 1]&&map[row][col])
			{
			    g++;
				score += map[row][col];
				map[row][col] *= 2;
				for (i = col - 1; i > 0; i--)
				{
					map[row][i] = map[row][i - 1];
					map[row][i - 1] = 0;
				}
			}
		}
	}
	if(k||g)
	Add(4);
}

//�ж���Ϸ�Ƿ���ֹ
int Judge()
{
	int row, col;
	for (row = 0; row < 4; row++)
	{
		for (col = 0; col < 4; col++)
		{
		    if(map[row][col] == 2048)
		      return 2;
		    if (map[row][col] == 0)
			  return 0;
		}
	}
	for (row = 0; row < 4; row++)
	{
		for (col = 0; col < 3; col++)
		{
			if ((map[row][col] == map[row][col + 1]) || (map[col][row] == map[col + 1][row]))
				return 0;
		}
	}
	return 1;
}
//������Ϸ
void Reset()
{
	FILE *fp;
	int i,j,temp;
	int a[10];
	if((fp = fopen("score.txt","r")) == NULL)
	{
		printf("���ļ�ʧ��!");
	}
	for(i = 0; i < 10; i++)
	{
		fscanf(fp,"%d",&a[i]);
	}
	fclose(fp);
	if (a[9] < score)
	{
		a[9] = score;
		for(i = 0; i < 9; i++)
		{
			for(j = 0; j < (9-i); j++)
			{
				if(a[j] < a[j+1])
				{
					temp = a[j];
					a[j] = a[j+1];
					a[j+1] = temp;
				}
			}
		}
		for(i = 0; i < 10; i++)
		{
			if(a[i] == score)
				break;
		}
		if(i == 0)
		{
			printf("\n\n\t\t\t\t��ϲ���������û������Ƽ�¼�ˣ���\n\n\n\n");
		}
		else
			printf("\n\n\t\t\t\t��ϲ��������а�!�������%d0%%���û�!\n",(10-i)%10);
	}
	else
		printf("\n\n\t\t\t\t���ź�û�н������а�!�ٽ�����!\n");
	if((fp=fopen("score.txt","w")) == NULL)
	{
		printf("���ļ�ʧ��!");
	}
	for(i = 0; i < 10; i++)
	{
		fprintf(fp,"%d\n",a[i]);
	}
	fclose(fp);
	score = 0;
	memset(map, 0, sizeof(map));
}
void List()
{
	FILE *fp;
	int a[10];
	int i;
	if((fp = fopen("score.txt","r")) == NULL)
	{
		printf("���ļ�ʧ��!");
		return ;
	}
	for(i = 0; i < 10; i++)
      fscanf(fp,"%d",&a[i]);
	fclose(fp);
	printf("\t\t\t   ������������������");
	printf("\n\t\t\t   ��    ���а�    ��\n");
	printf("\t\t\t   ������������������\n");
	for(i = 0; i < 10; i++)
	{
		if(a[i] != 0)
		{
			printf("\t\t\t   ����%-2d��:%-6d ��\n",i+1,a[i]);
			if((a[i+1] == 0) || i == 9)
				printf("\t\t\t   ������������������\n");
			else
				printf("\t\t\t   ������������������\n");
		}
	}
	system("pause");
	system("cls");
	Face();
}
