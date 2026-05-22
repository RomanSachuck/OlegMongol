using Cysharp.Threading.Tasks;

namespace Main.CodeBase.Utilities
{
    public delegate UniTask UniAction<T>(T param);
    public delegate UniTask UniAction<T1,T2>(T1 param, T2 param2);
    public delegate UniTask UniAction<T1,T2,T3>(T1 param, T2 param2, T3 param3);
    public delegate UniTask UniAction<T1,T2,T3,T4>(T1 param, T2 param2, T3 param3, T4 param4);
    public delegate UniTask UniAction<T1,T2,T3,T4,T5>(T1 param, T2 param2, T3 param3, T4 param4, T5 param5);
}