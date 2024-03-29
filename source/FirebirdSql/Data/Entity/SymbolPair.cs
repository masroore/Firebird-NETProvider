/*
 *  Firebird ADO.NET Data provider for .NET and Mono 
 * 
 *     The contents of this file are subject to the Initial 
 *     Developer's Public License Version 1.0 (the "License"); 
 *     you may not use this file except in compliance with the 
 *     License. You may obtain a copy of the License at 
 *     http://www.firebirdsql.org/index.php?op=doc&id=idpl
 *
 *     Software distributed under the License is distributed on 
 *     an "AS IS" basis, WITHOUT WARRANTY OF ANY KIND, either 
 *     express or implied.  See the License for the specific 
 *     language governing rights and limitations under the License.
 * 
 *  Copyright (c) 2008-2010 Jiri Cincura (jiri@cincura.net)
 *  All Rights Reserved.
 */

#if ((NET_35 && ENTITY_FRAMEWORK) || (NET_40))

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using System.Data.Metadata.Edm;
using System.Data.Common.CommandTrees;

using FirebirdSql.Data.FirebirdClient;

namespace FirebirdSql.Data.Entity
{
    /// <summary>
    /// The SymbolPair exists to solve the record flattening problem.
    /// <see cref="SqlGenerator.Visit(PropertyExpression)"/>
    /// Consider a property expression D(v, "j3.j2.j1.a.x")
    /// where v is a VarRef, j1, j2, j3 are joins, a is an extent and x is a columns.
    /// This has to be translated eventually into {j'}.{x'}
    /// 
    /// The source field represents the outermost SqlStatement representing a join
    /// expression (say j2) - this is always a Join symbol.
    /// 
    /// The column field keeps moving from one join symbol to the next, until it
    /// stops at a non-join symbol.
    /// 
    /// This is returned by <see cref="SqlGenerator.Visit(PropertyExpression)"/>,
    /// but never makes it into a SqlBuilder.
    /// </summary>
    internal class SymbolPair : ISqlFragment
    {
        #region � Fields �

        private Symbol source;
        private Symbol column;

        #endregion

        #region � Properties �

        public Symbol Source
        {
            get { return this.source; }
            set { this.source = value; }
        }

        public Symbol Column
        {
            get { return this.column; }
            set { this.column = value; }
        }

        #endregion

        #region � Constructors �

        public SymbolPair(Symbol source, Symbol column)
        {
            this.Source = source;
            this.Column = column;
        }

        #endregion

        #region � ISqlFragment Members �

        public void WriteSql(SqlWriter writer, SqlGenerator sqlGenerator)
        {
            // Symbol pair should never be part of a SqlBuilder.
            Debug.Assert(false);
        }

        #endregion
    }
}
#endif