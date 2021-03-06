﻿using CppSharp.AST;

namespace CppSharp.Passes
{
    public class FixDefaultParamValuesOfOverridesPass : TranslationUnitPass
    {
        public override bool VisitMethodDecl(Method method)
        {
            if (method.IsOverride && !method.IsSynthetized)
            {
                Method rootBaseMethod = ((Class) method.Namespace).GetBaseMethod(method);
                for (int i = 0; i < method.Parameters.Count; i++)
                {
                    if (rootBaseMethod.Parameters[i].DefaultArgument != null)
                    {
                        method.Parameters[i].DefaultArgument =
                            rootBaseMethod.Parameters[i].DefaultArgument.Clone();
                    }
                }
            }
            return base.VisitMethodDecl(method);
        }
    }
}
