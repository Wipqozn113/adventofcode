import queue

class Graph:
    def __init__(self, root=None):
        self.root = root
        self.nodes = set()

    def ResetNodes(self):
        for node in self.nodes:
            node.explored = False
            node.parent = None

    def AddNode(self, node):
        self.nodes.add(node)

    def PopulateNodes(self, nodes):
        self.nodes = set(nodes)

    def FindShortestUnweightedStart(self, goal, starts):
        start = None
        shortest = None
        for st in starts:            
            dist = self.UnweightedShortestPath(goal, st)
            # Goal was unreachable
            if dist is not None:
                if shortest is None or dist < shortest:
                    start = st
                    shortest = dist
            self.ResetNodes()

        return start, shortest

    def UnweightedShortestPath(self, goal, root=None):
        # Default to Graph Root
        if root is None:
            root = self.root
        
        node = self.BFS(goal, root)
        # Unreachable Goal
        if node is None:
            return None
        dist = 0
        n = node
        while n.parent is not None:
            n = n.parent
            dist += 1

        return dist

    def BFS(self, goal, root=None):
        # Default to Graph Root
        if root is None:
            root = self.root

        # Setup queue with root
        q = queue.Queue()
        root.explored = True
        q.put(root)

        while not q.empty():
            node = q.get()
            if node is goal:
                return node
            
            for child in node.children:
                if not child.explored:
                    child.explored = True
                    child.parent = node
                    q.put(child)

class Node:
    def __init__(self, obj = None):
        self.obj = obj
        self.children = []
        self.parent = None
        self.parents = []
        self.explored = False
    
    @property
    def id(self):
        if self.obj is not None:
            return self.obj.id
        else:
            return id(self)


