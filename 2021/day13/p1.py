class Paper:
    def __init__(self, height, width):
        self.height = height + 1
        self.width = width + 1
        self.paper =  [ [0]*self.width for i in range(self.height)]

    def AddDot(self, x, y):
        self.paper[y][x] += 1

    def Fold(self, comm):
        c = comm.strip().split("=")
        if c[0][-1] == "x":
            self.FoldX(int(c[1]))

        elif c[0][-1] == "y":
            self.FoldY(int(c[1]))

    def FoldX(self, f):
        for y in range(self.height):
            for x in range(f + 1, self.width):
                self.paper[y][x - ((x - f) * 2)] += self.paper[y][x]
        for y in range(self.height):
            self.paper[y] = self.paper[y][:f]
        self.width = f

    def FoldY(self, f):
        for y in range(f + 1, self.height):
            for x in range(self.width):
                self.paper[y - ((y - f ) * 2)][x] += self.paper[y][x]
        self.paper = self.paper[:f]
        self.height = f

    def TotalDots(self):
        n = 0
        for y in range(self.height):
            for x in range(self.width):
                n += (1 if self.paper[y][x] > 0 else 0)

        return n

    def PrintPaper(self):
        for row in self.paper:
            line = ""
            for column in row:
                line += "#" if column > 0 else "."
            print(line)

    def PrintDebugPaper(self):
        for p in self.paper:
            print(p)

height = 0
width = 0
dots = []
folds = []
with open('p1.in') as file:
    for line in file:
        if line[:4] == 'fold':
            folds.append(line)
        elif line.strip() == "":
            continue
        else: 
            d = line.strip().split(",")
            d[0] = int(d[0])
            d[1] = int(d[1])
            dots.append(d)
            if d[0] > width:
                width = d[0]
            if d[1] > height:
                height = d[1]

paper = Paper(height, width)
for dot in dots:
    paper.AddDot(dot[0], dot[1])

paper.Fold(folds[0])
print(paper.TotalDots())

