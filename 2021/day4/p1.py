class Coord:
    def __init__(self, x, y):
        self.x = x
        self.y = y

class Point:
    def __init__(self, value):
        self.val = value
        self.marked = False

    def Mark(self):
        self.marked = True

    def IsMarked(self):
        return self.marked

    def IsNotMarked(self):
        return not self.marked


class Tracker:
    def __init__(self, line):
        self.tracker = {}
        values = line.split(",")
        for val in values:
            self.tracker[val] = {}

    def AddBoard(self, board, index):
        for x in range(5):
            for y in range(5):
                val = board.Val(x, y)
                self.Add(val, index, x, y)                

    def Add(self, val, index, x, y):
        # Only track if it's a value that'll actually be called
        if(val in self.tracker):
            self.tracker[val][index] = Coord(x,y)

    def Find(self, val):
        return self.tracker[val]

class Board:
    def __init__(self, lines):
        self.board = []
        for line in lines:
            l = line.strip().split(" ")
            temp = []
            for sq in l:
                if(sq.strip() != ""):
                    temp.append(Point(sq))
            self.board.append(temp)

    def Val(self, x, y):
        return self.board[x][y].val

    def Mark(self, x, y):
        self.board[x][y].Mark()

    def CheckVictory(self, x, y):
        victory = True
        for i in range(5):
            if(self.board[i][y].IsNotMarked()):
                victory = False

        if not victory:
            victory = True
            for i in range(5):
                if(self.board[x][i].IsNotMarked()):
                    victory = False
                
        return victory

    def CalcScore(self, square):
        sum = 0
        for x in range(5):
            for y in range(5):
                if(self.board[x][y].IsNotMarked()):
                    sum += int(self.board[x][y].val)

        return sum * int(square)
    

class Boards:
    def __init__(self, values):
        self.boards = []
        self.tracker = Tracker(values)
        self.squares = []
        squares = values.split(",")
        for sq in squares:
            self.squares.append(sq.strip())

    def CreateBoard(self, lines):
        n = len(self.boards)
        board = Board(lines)
        self.boards.append(board)
        self.tracker.AddBoard(board, n)

    def FindWinner(self):
        for square in self.squares:
            boards = self.tracker.Find(square)            
            for key in boards:
                x = boards[key].x
                y = boards[key].y
                self.boards[key].Mark(x, y)

                if(self.boards[key].CheckVictory(x, y)):
                    return self.boards[key].CalcScore(square)


boards = None
buffer = []
with open('p1.in') as file:
    for line in file:
        if(boards is None):
            boards = Boards(line)
            continue
    
        if(line.strip() == ""):
            if(len(buffer) > 0):
                boards.CreateBoard(buffer)
                buffer = []
            continue

        buffer.append(line)

if(len(buffer) > 0):
    boards.CreateBoard(buffer)
    buffer = []

print(boards.FindWinner())

    

    

