using System;

namespace JuanRod.LevelGenerator.Code.Exceptions
{
    public class InvalidRuleDeclarationException : Exception
    {
        public override string Message => "Duplicate tag in special rule declaration, can only define one rule per tag";
    }
}
