class Cavern:
    def __init__(self):
        self.minrisk = None
        self.nodes = []    
        self.dist = {}
        self.flatnodes = {} 
        self.endnode = None

    def AddNodes(self, nodes):
        temp = []
        for risk in nodes:
            temp.append(Node(risk, self))
        self.nodes.append(temp)
    
    @property
    def Height(self):
        return len(self.nodes)

    @property
    def Width(self):
        return len(self.nodes[0])

    def LowestRisk(self):
        while len(self.flatnodes) > 0:
            dist = {x:self.dist[x] for x in self.dist if x in self.flatnodes}
            key = min(dist, key=dist.get)
            node = self.flatnodes.pop(key)
            for n in node.paths:
                temp = self.dist[key] + n.risk
                if temp < self.dist[n.name]:
                    self.dist[n.name] = temp
        
        print(self.dist[self.endnode.name])

    def AddPaths(self):
        for y in range(self.Height):
            for x in range(self.Width):
                self.dist[f"{y}-{x}"] = 999999999
                self.flatnodes[f"{y}-{x}"] = self.nodes[y][x]
                self.nodes[y][x].name = f"{y}-{x}"
                if x == 0 and y == 0:
                    self.dist[f"{y}-{x}"] = 0
                    self.nodes[y][x].isstart = True
                elif y == (self.Height - 1) and x == (self.Width - 1):
                    self.nodes[y][x].isend = True
                    self.endnode = self.nodes[y][x]

                if x + 1 < self.Width:
                    self.nodes[y][x].AddPath(self.nodes[y][x+1])
                if x - 1 >= 0:
                    self.nodes[y][x].AddPath(self.nodes[y][x-1])
                if y + 1 < self.Height:
                    self.nodes[y][x].AddPath(self.nodes[y + 1][x])
                if y - 1 >= 0:
                    self.nodes[y][x].AddPath(self.nodes[y - 1][x])
                

class Node:
    def __init__(self, risk, cavern):
        self.name = ""
        self.risk = int(risk)
        self.visited = False
        self.cavern = cavern
        self.paths = []
        self.isstart = False
        self.isend = False
        self.index = ""

    def AddPath(self, node):
        self.paths.append(node)

cavern = Cavern()
with open('p1.in') as file:
    for line in file:
        cavern.AddNodes(line.strip())

cavern.AddPaths()
cavern.LowestRisk()

