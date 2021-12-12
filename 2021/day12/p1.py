class Cave:
    def __init__(self):
        self.nodes = {}
        self.startnode = None
        self.endnode = None

    @property
    def Paths(self):
        return self.endnode.counter

    def FindPaths(self):
        self.startnode.Explore([])
        print("======")
        print(self.Paths)

    def AddNodes(self, n1, n2):
        if n1 not in self.nodes:
            self.nodes[n1] = Node(n1)
            if n1 == "start":
                self.startnode = self.nodes[n1]

            if n1 == "end":
                self.endnode = self.nodes[n1]

        if n2 not in self.nodes:
            self.nodes[n2] = Node(n2)
            if n2 == "start":
                self.startnode = self.nodes[n2]

            if n2 == "end":
                self.endnode = self.nodes[n2]
                
        self.nodes[n1].AddPath(self.nodes[n2])                
        self.nodes[n2].AddPath(self.nodes[n1])

class Node:
    def __init__(self, name):
        self.name = name
        self.isstart = True if name == "start" else False
        self.isend = True if name == "end" else False
        self.paths = []
        self.major = name.isupper()
        self.counter = 0
        self.visited = False

    @property
    def IsMajor(self):
        return self.major
    
    @property
    def IsMinor(self):
        return not self.major

    def AddPath(self, node):
        self.paths.append(node)

    def Explore(self, path):
        if self.isend:
            print(path)
            self.counter += 1
            return

        if self.IsMinor and self.visited:
            return

        self.visited = True
        path.append(self.name)
        for p in self.paths:
            p.Explore(path)

        path.remove(self.name)
        self.visited = False

    
cave = Cave()
with open('p1.in') as file:
    for line in file:
        l = line.strip().split("-")
        cave.AddNodes(l[0].strip(), l[1].strip())

cave.FindPaths()