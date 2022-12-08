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

    def PopulatePartners(self):
        self.partners = [self.bottom,self.left,self.right,self.top]

    def IsVisible(self):
        # Already checked
        if self.visibility_checked:
            return self.is_visible
        
        # Outer Edges trees always visible 
        if None in self.partners:
            self.is_visible = True
        else:
            for tree in self.partners:
                # A tree is Visible if its taller than at least one of its visible neighbours
                if self.height > tree.height:
                    self.is_visible = tree.IsVisible()
                    if self.is_visible:
                        break
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

trees = PopulateForest("test2.in")
count = CountVisibleTrees(trees)
print(count)
                
