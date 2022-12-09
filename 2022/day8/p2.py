class Tree:
    def __init__(self):
        self.height = None
        self.is_visible = None
        self.row = 0
        self.col = 0
        self.visibility_checked = False
        self.top = None
        self.bottom = None
        self.left = None
        self.right = None
        self.partners = []
        self.trees = {}
        self.tallest = {}
        self.scenic_score = None

    def PopulatePartners(self):
        self.partners = [self.bottom,self.left,self.right,self.top]
        self.trees["right"] = self.right
        self.trees["left"] = self.left
        self.trees["bottom"] = self.bottom
        self.trees["top"] = self.top
    
    def PopulateTallest(self):
        self.PopulateTallestDirection("right")
        self.PopulateTallestDirection("left")
        self.PopulateTallestDirection("bottom")
        self.PopulateTallestDirection("top")

    def PopulateTallestDirection(self, direction):
        tree = self.trees[direction]        
        if direction in self.tallest:
            return self.tallest[direction]        

        if tree is None:
            self.tallest[direction] = None
            return self.height
          
        tallest = tree.PopulateTallestDirection(direction)
        self.tallest[direction] = tallest if tallest is not None and tallest > tree.height else tree.height
                 
        return (self.height if (tallest is None) or (tallest < self.height) else tallest)       


    def IsVisible(self):
        # Already checked
        if self.visibility_checked:
            return self.is_visible

        self.PopulateTallest()
       # print(self, self.height, self.tallest, self.trees)
        
        # Outer Edges trees always visible 
        if None in self.partners:
            self.is_visible = True
        else:
            #if(self.row == 3 and self.col in [1, 3]):
                #print(self.tallest, self.right.height, self.bottom.height, self.left.height, self.top.height)
            for direction, tree in self.trees.items():
                if self.tallest[direction] is None or self.height > self.tallest[direction]:
                    self.is_visible = True
                    break
                else:
                    self.is_visible = False

        self.visibility_checked = True
        return self.is_visible

    def ViewingDistance(self, next_tree, direction, distance, height):
        if next_tree is None: # or next_tree.height >= height:
            return distance
        
        distance += 1
        if next_tree.trees[direction] is None or next_tree.height >= height:
            return distance

        return next_tree.ViewingDistance(next_tree.trees[direction], direction, distance, height)            

    def CalculateScenicScore(self):
        scenic_score = 1
        for direction, tree in self.trees.items():
            scenic_score *= self.ViewingDistance(tree, direction, 0, self.height)
            #print(self.height, direction, self.ViewingDistance(tree, direction, 0, self.height))
        
        return scenic_score

def FindHighestScenic(trees):
    highest = 0
    for row in trees:
        for tree in row:       
            score = tree.CalculateScenicScore()
            if score > highest:
                highest = score
    return highest

def PopulateForest(filename):
    height = sum(1 for line in open(filename))
    width = None
    row = 0
    col = 0
    with open(filename) as file:
        for line in file:
            line = line.strip()
            if width is None:
                width = len(line)
                trees = [[Tree() for i in range(width)] for j in range(height)]
            for l in line:
                trees[row][col].height = int(l)     
                trees[row][col].row = row
                trees[row][col].col = col           
                if row + 1 < height:
                    trees[row][col].bottom = trees[row + 1][col]
                else:
                    trees[row][col].bottom = None
                if row - 1 >= 0:
                    trees[row][col].top = trees[row - 1][col]
                else:
                    trees[row][col].top = None
                if col + 1 < width:
                    trees[row][col].right = trees[row][col + 1]
                else:
                    trees[row][col].right = None
                if col - 1 >= 0:
                    trees[row][col].left = trees[row][col - 1]
                else:
                    trees[row][col].left = None
                trees[row][col].PopulatePartners()
                col += 1
            row += 1
            col = 0
    #print(height, width)
    return trees

def CountVisibleTrees(trees):
    count = 0
    n = 0
    for row in trees:
        l = ""
        for tree in row:
            l += str(tree.height)
            n += 1            
            if tree.IsVisible():
                count += 1
               # l += "(T)"
            else:
               pass # l += "(F)"
     #   print(l)
    #print(n)
    return count

trees = PopulateForest("input.in")
count = FindHighestScenic(trees)
print(count)
                
