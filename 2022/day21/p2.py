from sympy import symbols, solve
from sympy.parsing.sympy_parser import parse_expr

class Monkey:
    def __init__(self, name, job):
        self.name = name.strip()
        self.job = job
        self.val = None
        if isinstance(job, int):
            self.val = job
        else:
            self.mon1 = None
            self.mon1name = job[0].strip()
            self.mon2 = None
            self.mon2name = job[2].strip()
            self.op = job[1].strip()
            if name == "root":
                self.op = "="

    def FinishJob(self, monkeys):
        if self.val is None:
            self.mon1 = monkeys[self.mon1name]
            self.mon2 = monkeys[self.mon2name]

    def CreateFullJob(self):
        return self.JobsToString()

    def JobsToString(self):
        if self.name == "humn":
            return "x"

        if self.val is not None:
            return str(self.val)        

        mon1 = self.mon1.JobsToString()
        mon2 = self.mon2.JobsToString()
        job = "({} {} {})".format(mon1, self.op, mon2)

        return job

    def RootJob(self):
        return self.mon1.DoJob() == self.mon2.DoJob()

    def DoJob(self):
        if self.name == "root":
            return self.RootJob()

        if self.name == "humn":
            return self.val

        if self.val is not None:
            return self.val

        job = "self.mon1.DoJob() {} self.mon2.DoJob()".format(self.op)

        return eval(job)

def CreateMonkeys(filename):
    monkeys = {}
    with open(filename) as file:
        for line in file:
            name, job = line.strip().split(":")
            jobs = job.strip().split(" ")
            if len(jobs) == 1:
                jb = int(jobs[0])
            else:
                jb = jobs
            monk = Monkey(name.strip(), jb)
            monkeys[monk.name] = monk
    
    for key, monkey in monkeys.items():
        monkey.FinishJob(monkeys)

    return monkeys

# 142000
def BruteForceHumnNumber(monkeys):
    humn = monkeys["humn"]
    root = monkeys["root"]
    num = 0
    while True:
        if num % 1000 == 0:
            print("Testing number ", num)
        humn.val = num
        if root.RootJob():
            print(num)
            return num
        num += 1

# Because fucking syntax error annoyance
# https://www.mathpapa.com/algebra-calculator.html
def DiscoverHumnNumber(monkeys):
    root = monkeys["root"]
    equation = root.CreateFullJob()
    equations = equation.split("=")
    eqX = equations[0] if 'x' in equations[0] else equations[1]
    expr = parse_expr(eqX)
    sol = solve(expr)
    print(sol[0])

monkeys = CreateMonkeys("input.in")
DiscoverHumnNumber(monkeys)
