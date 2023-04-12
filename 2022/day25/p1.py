class SNAFU:
    def __init__(self):
        self.snafu = None
        self.dec = None

    def SetSnafu(self, snafu):
        self.snafu = snafu
    
    def SetDecimal(self, decimal):
        self.dec = decimal

    def SnafuAddition(self, other):
        carry_the_one = False
        sn1 = self.ConvertToIntList(self.snafu)
        sn2 = self.ConvertToIntList(other.snafu)
        result = []
        i = len(sn1) if len(sn1) > len(sn2) else len(sn2)
        for n in range(1, i+1):
            val1 = 0 if n > len(sn1) else sn1[-n]
            val2 = 0 if n > len(sn2) else sn2[-n]
            sn3 = val1 + val2  
            result.insert(0, sn3)
                   
        print(result)

    def _CarryTheOne(self, val):
        if abs(sn3) == 3:
            sn3 = -2 if sn3 > 0 else 2
        elif abs(sn3) == 4:
            sn3 = -1 if sn3 > 0 else 1

    def ConvertToIntList(self, convert):
        snafu = []
        for sn in convert:
            if sn == "-":
                sn = -1
            elif sn == "=":
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

def Tests():
    snafu1 = SNAFU()
    snafu1.SetSnafu("1=")
    snafu2 = SNAFU()
    snafu2.SetSnafu("1-")
    snafu1.SnafuAddition(snafu2)

Tests()
total = CalculateSum()
print("Decimal: ", total.AsDecimal)
print("SNAFU: ", total.AsSnafu)