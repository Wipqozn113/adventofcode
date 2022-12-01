class Variable:
    def __init__(self, name):
        self.name = name
        self.val = 0

class Alu:
    def __init__(self, commands):
        self.largest = 0
        self.variables = {"w": Variable("w"), "x":  Variable("x"), "y": Variable("y"), "z": Variable("z")}
        self.commands = commands
        self.groupedcommands = self.GroupCommands(commands)
        self.states = {}

    def Save(self, id):
        self.states[id] = {"w": self.variables["w"].val, "x":  self.variables["x"].val, "y": self.variables["y"].val, "z": self.variables["z"].val}

    def Load(self, id):
        if id in self.states:
            state = self.states[id]
            self.variables["z"].val = state["z"]

    def Delete(self, id):
        del(self.states[id])
    
    def GroupCommands(self, commands):
        gcomms = []
        gcomm = []
        for command in commands:
            comm = command[:3]
            if comm == "inp" and len(gcomm) > 0:
                gcomms.append(gcomm)
                gcomm = []

            gcomm.append(command)

        gcomms.append(gcomm)

        return gcomms        

    def FindLargest(self):
        self.Execute(0, "")

        return self.largest
    
    def Execute(self, group, num):
        for a in range (1, 10):
            numcurr = num + str(a)
            try:
                for comm in self.groupedcommands[group]:
                    self.ExecuteCommand(comm, a)
            # Invalid command, continue to next input
            except ValueError:
                continue
            self.Save(numcurr)
            print(self.variables["z"].val % 26)

            """
                        self.Save(numcurr)
                        if len(numcurr) < 14: 
                            self.Execute(group + 1, numcurr)
                            self.Delete(numcurr)
                        elif self.variables["z"].val == 0:
                            print("oh g oodie")            
                            val = int(numcurr)
                            self.largest = val if val > self.largest else self.largest
            """
   

    def ExecuteCommand(self, command, val):
        comm = command.strip().split(" ")
        if len(comm) == 2:
            b = val
        else:
            b = int(comm[2]) if comm[2] not in self.variables else self.variables[comm[2]]

        if comm[0] == "inp":
            self.Inp(self.variables[comm[1]], b)
        
        elif comm[0] == "mul":
            self.Mul(self.variables[comm[1]], b)

        elif comm[0] == "add":
            self.Add(self.variables[comm[1]], b)

        elif comm[0] == "div":
            self.Div(self.variables[comm[1]], b)

        elif comm[0] == "mod":
            self.Mod(self.variables[comm[1]], b)

        elif comm[0] == "eql":
            self.Eql(self.variables[comm[1]], b) 

    def Inp(self, a, v):
        a.val = v

    def Add(self, a, b):
        valb = b if not isinstance(b, Variable) else b.val
        a.val = a.val + valb

    def Mul(self, a, b):
        valb = b if not isinstance(b, Variable) else b.val
        a.val = a.val * valb

    def Div(self, a, b):
        valb = b if not isinstance(b, Variable) else b.val
        if valb == 0:
            raise ValueError(f"Division by Zero {a.val} {valb}")

        a.val = round(a.val // valb)

    def Mod(self, a, b):
        valb = b if not isinstance(b, Variable) else b.val
        if a.val < 0 or valb <= 0:
            raise ValueError(f"Mod by 0 {a.val} {valb}")
        a.val = a.val % valb

    def Eql(self, a, b):
        valb = b if not isinstance(b, Variable) else b.val
        a.val = 1 if a.val == valb else 0

commands = []
with open('p1.in') as file:
    for line in file:
        commands.append(line)

alu = Alu(commands)
print(alu.FindLargest())
        
