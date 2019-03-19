#include<stdio.h>
#include<conio.h>
#include<time.h>
#include<stdlib.h>
#include<windows.h>
#define M 25          // ��
#define N 50          // ��
#define L 3           //�Ѷ�ϵ��
struct Node
{
	int x;
	int y;
	short flag;  // ��ǲ�λ
	struct Node* next;
};
struct Node  typedef *link;
void MoveUDLR(int, int);
void Eat(int, int);
void SetMap(int);
void Start(int);
void Setting(int);
void SettingFace();
void PrintMap();
void Setscore();
void Food();
void Move();
void Menu();
void Rank();
void HideCursor();
int GameOver = 1;     // �ж���Ϸ����   1��û����  0������
int score;            //��ǰ�÷�
int len = 0;         //������
int Mode = 0;        //��ǰ��ͼģʽ
int Flag = 0;        // ���ִ�����ѵ��ģʽ   0������  ��0��ѵ��
int map[M][N] = { 0 };
link head = NULL;
//������
int main()
{
	HideCursor();
	while (1)
	 Menu();
	return 0;
}
//��Ϸ�˵�
void Menu()
{
	system("cls");
	system("title ̰����");
	printf("\n\n\n\n");
	printf("\t\t\t\t\t̰����\n\n");
	printf("\t\t\t������������������������������������\n");
	printf("\t\t\t��             1.Start            ��\n");
	printf("\t\t\t������������������������������������\n");
	printf("\t\t\t��             2.Rank             ��\n");
	printf("\t\t\t������������������������������������\n");
	printf("\t\t\t��             3.Setting          ��\n");
	printf("\t\t\t������������������������������������\n");
	printf("\t\t\t��             0.Exit             ��\n");
	printf("\t\t\t������������������������������������\n");
	int n;
	scanf("%d", &n);
	system("cls");
	switch (n)
	{
	case 1: Start(Mode); break;
	case 2: Rank(); break;
	case 3: SettingFace();  break;
	case 0: exit(0);
	}
}
//��ʼ��Ϸ
void Start(int n)
{
	system("cls");
	GameOver = 1;
	len = 0;
	Setting(Mode);
	SetMap(n);
	PrintMap();
	_sleep(200);
	COORD pos = { 0, 0 };        //����
	SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE), pos);
	getch();
	Move();
}
//��Ϸģʽѡ��
void SettingFace()
{
	COORD pos = { 0, 0 };                           //����
	SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE), pos);
	printf("\n\n\n\n");
	printf("\t\t\t������������������������������������\n");
	printf("\t\t\t��           1.Easy ѵ����        ��\n");
	printf("\t\t\t������������������������������������\n");
	printf("\t\t\t��           2.Normal ѵ����      ��\n");
	printf("\t\t\t������������������������������������\n");
	printf("\t\t\t��           3.Hard ѵ����        ��\n");
	printf("\t\t\t������������������������������������\n");
	printf("\t\t\t��           0.So Easy ����       ��\n");
	printf("\t\t\t������������������������������������\n");
	scanf("%d", &Mode);
	Setting(Mode);
	Flag = Mode;
	//Menu();
}
//��Ƶ�ͼģʽ
void Setting(int n)
{
	int i, j, k;
	for (i = 0; i < M; i++)
	for (j = 0; j < N; j++)
	{
		if (i == 0 || i == M - 1 || j == 0 || j == N - 1)
			map[i][j] = -1;
		else
			map[i][j] = 0;
	}
	switch (n)
	{
	case 1:{
			   for (j = N / 5; j < 4 * N / 5; j++)
			   {
				   map[N / 6][j] = -1;
				   map[N / 3][j] = -1;
			   }
	} break;
	case 2:{
			   for (j = N / 5; j < 4 * N / 5; j++)
			   {
				   map[N / 6][j] = -1;
				   map[N / 3][j] = -1;
			   }
			   for (i = 5; i<20; i++)
				   map[i][25] = -1;
	} break;
	case 3:
	{
			  for (j = 10; j<40; j++)
			  {
				  map[10][j] = -1;
				  map[15][j] = -1;
			  }
			  for (j = 5, k = 35; j<15; j++, k++)
			  {
				  map[5][j] = -1;
				  map[20][j] = -1;
				  map[5][k] = -1;
				  map[20][k] = -1;
			  }
			  for (i = 5; i <= 20; i++)
			  {
				  map[i][5] = -1;
				  map[i][45] = -1;
			  }
	}
		break;
	case 0:break;
	}
}
//��ӡ��ͼ
void PrintMap()
{
	int i, j;
	int theBest = 0;          //��߷�
	FILE *fp;
	int a[4][10];
	if ((fp = fopen("score.txt", "r")) == NULL)
	{
		printf("���ļ�ʧ��!");
		return;
	}
	for (i = 0; i < 4; i++)
	for (j = 0; j < 10; j++)
		fscanf(fp, "%d", &a[i][j]);
	fclose(fp);
	if (Flag == 0)
		theBest = a[3][0];
	else
		theBest = a[Mode - 1][0];
	printf("\t\t\t\t\t\t�� %d �� \t\n\n", Mode + 1);
	printf("\t\t\t��߷�Ϊ:%d\t\t\t��ǰ����Ϊ:%d\n", theBest, score);
	for (i = 0; i < M; i++)
	{
		printf("\t\t\t");
		for (j = 0; j < N; j++)
		{
			if (map[i][j] == -1)
				printf("#");
			else if (map[i][j] == 0)
				printf(" ");
			else if (map[i][j] == 1)
				printf("*");          //ʳ��
			else if (map[i][j] == 2)
				printf("O");          //��ͷ
			else if (map[i][j] == 3)
				printf("=");          //����
		}
		printf("\n");
	}
}
//��ʼ����ͼ
void SetMap(int n)
{
	Setting(n);
	int row, col;
	head = (link)malloc(sizeof(struct Node));
	srand((unsigned int)time(NULL));
	while (1)
	{
		row = rand() % (M - 2) + 1;
		col = rand() % (N - 2) + 1;
		if (map[row][col] == 0)
		{
			map[row][col] = 1;   // ���ֲ���ʳ��
			break;
		}
	}
	do{
		row = rand() % (M - 2) + 1;
		col = rand() % (N - 2) + 1;
		if (map[row][col] == 0)
		{
			map[row][col] = 2;
			head->x = col;
			head->y = row;
			head->flag = 2;
			head->next = NULL;
			break;
		}
	} while (1);             // ���ֵ���ͷ
}
//�߳�ʳ��
void Eat(int row, int col)
{
	link p;
	p = (link)malloc(sizeof(struct Node));
	p->x = col;
	p->y = row;
	p->flag = 2;
	map[row][col] = 2;
	p->next = head;
	head = p;
	len++;
}
//�������ʳ��
void Food()
{
	int row, col;
	do{
		row = rand() % (M - 2) + 1;
		col = rand() % (N - 2) + 1;
		if (map[row][col] == 0)
		{
			map[row][col] = 1;
			break;
		}
	} while (1);
}
//�����ƶ������
void Move()
{
	char ch;
	int row, col;
	if (kbhit() != 0)
	{
		while (kbhit() != 0)
			ch = getch();
		switch (ch)
		{
		case 72: case 'w': MoveUDLR(-1, 0); break;   //��
		case 80: case 's': MoveUDLR(1, 0); break;   //��
		case 75: case 'a': MoveUDLR(0, -1); break;   //��
		case 77: case 'd': MoveUDLR(0, 1); break;   //��
		}
	}
}
//ʵ�ָ������ƶ�
void MoveUDLR(int x, int y)
{
	int row, col;
	while (GameOver)
	{
		col = head->x;
		row = head->y;
		if (map[row + x][col + y] == 0)
		{
			link temp = (link)malloc(sizeof(struct Node));
			temp->x = col + y;
			temp->y = row + x;
			temp->flag = 2;
			temp->next = head;
			head = temp;
			link p = head, q = head;
			while (p->next->next)
				p = p->next;
			map[p->next->y][p->next->x] = 0;
			free(p->next);
			p->next = NULL;
			while (q)
			{
				if (q == head)
					map[q->y][q->x] = 2;
				else
					map[q->y][q->x] = 3;
				q = q->next;
			}
		}
		else if (map[row + x][col + y] == 1)
		{
			Eat(row + x, col + y);
			score += (Mode + len);     //�������  Mode + len; 
			Food();
		}
		else if (map[row + x][col + y] == 2 || map[row + x][col + y] == 3 || map[row + x][col + y] == -1)
			GameOver = 0;
		PrintMap();
		if (100 - L*len*(Mode + 1) >= 0)
		{
			if (y)
				Sleep(100 - L*len*(Mode + 1));
			else
				Sleep(300 - L*len*(Mode + 1));
		}
		else
		{
			if (y)
				Sleep(6);
			else
              Sleep(10);
		}
			

		if (GameOver)
		{
			COORD pos = { 0, 0 };                           //����
			SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE), pos);
		}
		else
		{
			Setscore();
			printf("\t\t��Ϸ����������һ�֣�����0/1��\n");
			int n;
			scanf("%d", &n);
			if (n)
			{
				score = 0;
				Start(Mode);
			}
		}
		if (len >= (Mode + 1)*L&&!Flag)
		{
			printf("\t\t��ϲ�㣬���سɹ������ڽ�����һ�ء�����\n");
			_sleep(1000);
			if (Mode <= 2)
				Start(++Mode);
			else
			{
				system("cls");
				printf("��ϲ�㣡ͨ���ˣ�\n");
				Setscore();
				_sleep(2000);
				Mode = 0;
			}
		}
		if (kbhit() != 0)
			Move();
	}
}
//�����ļ����а�
void Setscore()
{
	int a[4][10];   //  ��ά���鴢�����а�
	int i, j, temp, k;
	FILE *fp;
	if (Mode == 0||Flag == 0)
		k = 3;
	else
		k = Mode - 1;
	if ((fp = fopen("score.txt", "r")) == NULL)
	{
		printf("\t\t\t���ļ�ʧ��!\n\t\t\t�ļ�score.txtû���ҵ�");
		return;
	}
	for (i = 0; i < 4; i++)
	{
		for (j = 0; j < 10; j++)
			fscanf(fp, "%d\n", &a[i][j]);
	}
	if (a[k][9] < score)
	{
		a[k][9] = score;
		for (i = 0; i < 9; i++)
		{
			for (j = 0; j < (9 - i); j++)
			{
				if (a[k][j] < a[k][j + 1])
				{
					temp = a[k][j];
					a[k][j] = a[k][j + 1];
					a[k][j + 1] = temp;
				}
			}
		}
		for (j = 0; j < 10; j++)
		{
			if (a[k][j] == score)
				break;
		}
		if (j == 0)
		{
			printf("\n\n\t\t\t\t��ϲ���������û������Ƽ�¼�ˣ���\n\n\n\n");
		}
		else
			printf("\n\n\t\t\t\t��ϲ��������а�!�������%d0%%���û�!\n", (10 - j) % 10);
	}
	else
		printf("\n\n\t\t\t\t���ź�û�н������а�!�ٽ�����!\n");
	if ((fp = fopen("score.txt", "w")) == NULL)
	{
		printf("���ļ�ʧ��!");
	}
	for (i = 0; i < 4; i++)  
	{
		for (j = 0; j < 10; j++)
			fprintf(fp, "%d\n", a[i][j]);
	}
	fclose(fp);
	score = 0;
}
//���а�
void Rank() 
{
	FILE *fp;
	int a[4][10];
	int i, j, k;               //  k ��ʾ�ڼ������а�
	if (Mode==0||Flag == 0)
		k = 3;
	else
		k = Mode - 1;
	if ((fp = fopen("score.txt", "r")) == NULL)
	{
		printf("���ļ�ʧ��!");
		return;
	}
	for (i = 0; i < 4; i++)
	for (j = 0; j < 10; j++)
		fscanf(fp, "%d", &a[i][j]);
	fclose(fp);
	printf("\t\t\t   ������������������\n");
	if (Flag == 0)
		printf("\t\t\t   ��  �������а�  ��\n");
	else
		printf("\t\t\t   ��  Mode%d���а� ��\n", Mode);
	printf("\t\t\t   ������������������\n");
	for (j = 0; j < 10; j++)
	{
		if (a[k][j] != 0)
		{
			printf("\t\t\t   ����%-2d��:%-6d ��\n", j + 1, a[k][j]);
			if ((a[k][j + 1] == 0) || j == 9)
				printf("\t\t\t   ������������������\n");
			else
				printf("\t\t\t   ������������������\n");
		}
	}
	system("pause");
	system("cls");
}
//���ع��
void HideCursor()
{
	CONSOLE_CURSOR_INFO cursor_info = { 1, 0 };
	SetConsoleCursorInfo(GetStdHandle(STD_OUTPUT_HANDLE), &cursor_info);
}