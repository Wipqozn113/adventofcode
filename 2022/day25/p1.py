class SNAFU:
    def __init__(self):
        self.snafu = None
        self.dec = None

    def SetSnafu(self, snafu):
        self.snafu = snafu
    
    def SetDecimal(self, decimal):
        self.dec = decimal

    def SnafuAddition(self, other):
        sn1 = self.ConvertToIntList(self.snafu)
        sn2 = self.ConvertToIntList(other.snafu)
        result = []
        i = len(sn1) if len(sn1) > len(sn2) else len(sn2)
        for n in range(1, i+1):
            sn3 = sn1[-n] + sn2[-n]
            if -2 > sn3 > 2:
                #


    def ConvertToIntList(self, convert):
        snafu = []
        for sn in convert
            if sn == "-":
                sn = -1
            elif sn == "-2":
                sn = -2
            else:
                sn = int(sn)
            snafu.append(sn)
        return snafu


    @property
    def AsSnafu(self):
        if self.snafu is not None:
            return self.snafu
        elif self.dec is not None:
            return self._ConvertToSnafu()
        else:
            return None

    @property
    def AsDecimal(self):
        if self.dec is not None:
            return self.dec
        elif self.snafu is not None:
            return self._ConvertToDecimal()
        else:
            return None
        
    def _ConvertToSnafu(self):
        if self.snafu is not None:
            return self.snafu

        if self.dec is None:
            return ""

        return self.snafu  
        
    def _ConvertToDecimal(self):
        if self.dec is not None:
            return self.dec

        if self.snafu is None:
            return 0            

        i = 0
        ln = len(self.snafu)
        num = 0
        while i < ln:
            x = ln - i - 1
            n = 0
            if self.snafu[i] == "-":
                n = -1
            elif self.snafu[i] == "=":
                n = -2
            else:
                n = int(self.snafu[i])
            
            num += (5**x) * n
            i += 1

        self.dec = num
        return self.dec       

def CalculateSum(filename = "test.in"):
    with open(filename) as file:
        total = 0
        for line in file:
            snafu = SNAFU()
            snafu.SetSnafu(line.strip())
            total += snafu.AsDecimal

    snafu = SNAFU()
    snafu.SetDecimal(total)

    return snafu

total = CalculateSum()
print("Decimal: ", total.AsDecimal)
print("SNAFU: ", total.AsSnafu)