#include <stdio.h>
#include <time.h>
#include <stdlib.h>
#include <conio.h>
#include <string.h>
#include <windows.h>      // 减少回闪清屏时
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
int score = 0;     //分数
int theBest;       //最高纪录
int main()
{
	Face();
	return 0;
}
void Face()
{
    printf("\n\n\n\n");
	printf("\t\t\t\t\t2048\n\n");
    printf("\t\t\t┌────────────────┐\n");
	printf("\t\t\t│             1.Start            │\n");
	printf("\t\t\t├────────────────┤\n");
	printf("\t\t\t│             2.Rank             │\n");
	printf("\t\t\t├────────────────┤\n");
	printf("\t\t\t│             3.Help             │\n");
	printf("\t\t\t├────────────────┤\n");
	printf("\t\t\t│             0.Exit             │\n");
	printf("\t\t\t└────────────────┘\n");
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
	printf("\n\n开始时棋盘内随机出现两个数字，出现的数字仅可能为2或4,玩家可以选择上下左右四个方向，若棋盘内的数字出现位移或合并，视为有效移动,玩家选择的方向上若有相同的数字则合并，每次有效移动可以同时合并，但不可以连续合并.合并所得的所有新生成数字相加即为该步的有效得分,玩家选择的方向行或列前方有空格则出现位移,每有效移动一步，棋盘的空位(无数字处)随机出现一个数字(依然可能为2或4).棋盘被数字填满，无法进行有效移动，判负，游戏结束;棋盘上出现2048，判胜，游戏结束\n");
    printf("回车返回上一层！\n");
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
			SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE), pos);   //减少清屏带来的闪烁
		}
		else
			break;
	}

	Print();
	char flag;
	printf("\t\t\t游戏结束!最终得分为%d \n", score);
	Reset();
	printf("\t\t\t是否要再来一局?[0/1]");
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
		printf("最高纪录文件没有找到！\n");
		exit(0);
	}
	fscanf(fp, "%d", &theBest);
	fclose(fp);
	printf("\n\n\n\n");
	printf("\t\t\t  最高分为:%d\t   当前分数为:%d\n",theBest,score);
	printf("\t\t\t-----------------------------------\n\n");
	printf("\t\t\t┌───┬───┬───┬───┐\n");
	for(i = 0; i < 4; i++)
	{
		printf("\t\t\t│");
		for(j = 0; j < 4; j++)
		{
			if(map[i][j] != 0)
					printf("%-6d│", map[i][j]);
			else
				printf("      │");
		}
		printf("\n");
		if(i < 3)
			printf("\t\t\t├───┼───┼───┼───┤\n");
	}
	printf("\t\t\t└───┴───┴───┴───┘\n");
}
//增加数值的函数
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
//控制函数 返回方向
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
	int k = 0,g = 0;       //计数器
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

//判断游戏是否终止
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
//重置游戏
void Reset()
{
	FILE *fp;
	int i,j,temp;
	int a[10];
	if((fp = fopen("score.txt","r")) == NULL)
	{
		printf("打开文件失败!");
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
			printf("\n\n\t\t\t\t恭喜你打败所有用户并且破纪录了！！\n\n\n\n");
		}
		else
			printf("\n\n\t\t\t\t恭喜你进入排行榜!并打败了%d0%%的用户!\n",(10-i)%10);
	}
	else
		printf("\n\n\t\t\t\t很遗憾没有进入排行榜!再接再厉!\n");
	if((fp=fopen("score.txt","w")) == NULL)
	{
		printf("打开文件失败!");
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
		printf("打开文件失败!");
		return ;
	}
	for(i = 0; i < 10; i++)
      fscanf(fp,"%d",&a[i]);
	fclose(fp);
	printf("\t\t\t   ┌───────┐");
	printf("\n\t\t\t   │    排行榜    │\n");
	printf("\t\t\t   ├───────┥\n");
	for(i = 0; i < 10; i++)
	{
		if(a[i] != 0)
		{
			printf("\t\t\t   │第%-2d名:%-6d │\n",i+1,a[i]);
			if((a[i+1] == 0) || i == 9)
				printf("\t\t\t   └───────┘\n");
			else
				printf("\t\t\t   ├───────┥\n");
		}
	}
	system("pause");
	system("cls");
	Face();
}
