class Tree:
    def __init__(self):
        self.height = None
        self.is_visible = None
        self.visibility_checked = False
        self.top = None
        self.bottom = None
        self.left = None
        self.right = None
        self.partners = []
        self.trees = {}
        self.tallest = {}

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
            return None
          
        tallest_after_me = tree.PopulateTallestDirection(direction)
        tallest = tallest_after_me
        self.tallest[direction] = tallest
                 
        return (self.height if (tallest is None) or (tallest < self.height) else tallest)       


    def IsVisible(self):
        # Already checked
        if self.visibility_checked:
            return self.is_visible

        self.PopulateTallest()
        
        # Outer Edges trees always visible 
        if None in self.partners:
            self.is_visible = True
        else:
            for direction, tree in self.trees.items():
                if self.tallest[direction] is None or self.height > self.tallest[direction]:
                    self.is_visible = True
                else:
                    self.is_visible = False

        self.visibility_checked = True
        return self.is_visible

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

    return trees

def CountVisibleTrees(trees):
    count = 0
    for row in trees:
        for tree in row:
            if tree.IsVisible():
                count += 1

    return count

trees = PopulateForest("test.in")
count = CountVisibleTrees(trees)
print(count)
                
