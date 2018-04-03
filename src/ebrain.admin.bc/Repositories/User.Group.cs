using ebrain.admin.bc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

/*
 * Author:          John Pham
 * CreatedDate Date:    07/23/2013
 * 
 */
namespace ebrain.admin.bc.Repositories
{
    public class UserGroupRepository : Repository<UserGroup>
    {
        public long Total { get; private set; }

        private ApplicationDbContext appContext
        {
            get { return (ApplicationDbContext)_context; }
        }

        public UserGroupRepository(ApplicationDbContext appContext) : base(context)
        {
        }

        public bool Update(UserGroup value, bool fullPermission = false)
        {
            var m_Ret = new bool();

            if (value != null)
            {
                using (var appContext = new Entities(this.ConnectionString))
                {
                    var cus = appContext.Edit< UserGroup>(x => x.ID == value.ID);

                    if (cus == null)
                    {
                        cus = new UserGroup
                        {
                            ID = value.ID = Guid.NewGuid(),

                            Code = new Setting.Configuration(this.ConnectionString).NewID(ConfigurationTable.UserGroup),
                            CreatedDate = DateTime.Now,
                        };
                        //
                        appContext.Add(cus);
                        this.Setup(fullPermission);
                        this.TakeOutAccessRight(fullPermission);
                        this.TakeInAccessRight(fullPermission);
                        this.InventoryAccessRight(fullPermission);
                        this.FundsAccessRight(fullPermission);
                    }

                    //user.Code = value.Code;
                    cus.Name = value.Name;
                    cus.Description = value.Description;
                    cus.UpdatedDate = DateTime.Now;

                    //
                    if(m_Ret.Value = appContext.Save() > 0)
                    {
                        m_Ret.ReturnID = cus.ID;
                    }
                }
            }

            return m_Ret;

        }

        public bool Delete(Guid index)
        {
            var m_Ret = new bool();

            using (var appContext = new Entities(this.ConnectionString))
            {
                var item = appContext.Delete< UserGroup>(x => x.ID == index);

                if (item != null && appContext.Users.Count(x => x.GroupID == index) <= 0)
                {
                    appContext.Delete<AccessRight>(x => x.GroupID == index);

                    if(m_Ret.Value = appContext.Save() > 0)
                    {
                        m_Ret.ReturnID = index;
                    }
                }
                else
                {
                    m_Ret.ErrorKey = "UGP_MSG1";
                }
            }

            return m_Ret;
        }

        public IList<UserGroup> Search(string name, string value, int page, int size)
        {
            var m_Ret = new List<UserGroup>();

            using (var appContext = new Entities(this.ConnectionString))
            {
                var items = from c in appContext.UserGroups
                            select c;

                //FILTER
                if (!string.IsNullOrEmpty(value))
                {
                    if (!string.IsNullOrEmpty(name)) name = name.ToUpper();

                    switch (name)
                    {
                        case "CODE":
                            items = items.Where(x => x.Code == value);

                            break;
                        case "NAME":
                            items = items.Where(x => x.Name.Contains(value));

                            break;
                        case "DESCRIPTION":
                            items = items.Where(x => x.Description.Contains(value));

                            break;
                        default:
                            items = items.Where(x => x.Code == value ||
                                x.Name.Contains(value) ||
                                x.Description.Contains(value));

                            break;
                    }
                }

                this.Total = items.Count();
                //
                if (size > 0 && page >= 0)
                {
                    items = (from c in items
                             orderby c.CreatedDate
                             select c).Skip(page * size).Take(size);
                }

                foreach (var item in items)
                {
                    m_Ret.Add(new UserGroup
                    {
                        ID = item.ID,
                        Code = item.Code,
                        Name = item.Name,
                        Description = item.Description
                    });
                }
            }

            return m_Ret;
        }

        public UserGroup GetItem(Guid index)
        {
            var m_Ret = default(UserGroup);

            using (var appContext = new Entities(this.ConnectionString))
            {
                var item = appContext.UserGroups.FirstOrDefault(x => (x.ID == index));

                if (item != null)
                {
                    m_Ret = new UserGroup
                    {
                        ID = item.ID,
                        Code = item.Code,
                        Name = item.Name,
                        Description = item.Description
                    };
                }
            }

            return m_Ret;
        }

        public long GetTabIndex()
        {
            var m_Ret = default(long);

            using (var appContext = new Entities(this.ConnectionString))
            {
                var item = appContext.UserGroups.Select(x => x.TabIndex).DefaultIfEmpty(0).Max();
                m_Ret = item + 1;

            }

            return m_Ret;
        }

        private bool Setup(bool fullPermission)
        {
            var m_Ret = false;
            var m_Type = new Crm.Setting.AccessRight(this.ConnectionString);

            if (m_Ret = (m_Type.Count(value.ID) <= 0))
            {
                //
                m_Type.Value = new Caicho.AccessRight
                {
                    View = true,
                    Edit = fullPermission,
                    Delete = fullPermission,
                    Create = fullPermission,
                    GroupID = value.ID,
                    FeatureID = new Guid("{19010B39-3631-44DA-93BD-687109EDD089}")
                };
                if (m_Type.Update().Value)
                {
                }

                //
                m_Type.Value = new Caicho.AccessRight
                {
                    View = true,
                    Edit = fullPermission,
                    Delete = false,
                    Create = fullPermission,
                    GroupID = value.ID,
                    FeatureID = new Guid("{3A221862-9C4B-462F-A894-6C906B841A42}")
                };
                if (m_Type.Update().Value)
                {
                }

                //
                m_Type.Value = new Caicho.AccessRight
                {
                    View = true,
                    Edit = fullPermission,
                    Delete = false,
                    Create = fullPermission,
                    GroupID = value.ID,
                    FeatureID = new Guid("{777CD779-4693-4B13-8CEA-0C83B7D88A8D}")
                };
                if (m_Type.Update().Value)
                {
                }

                //
                m_Type.Value = new Caicho.AccessRight
                {
                    View = true,
                    Edit = fullPermission,
                    Delete = fullPermission,
                    Create = fullPermission,
                    GroupID = value.ID,
                    FeatureID = new Guid("{65499107-0A44-4B85-8C49-22647E6CFA26}")
                };
                if (m_Type.Update().Value)
                {
                }

                //
                m_Type.Value = new Caicho.AccessRight
                {
                    View = true,
                    Edit = fullPermission,
                    Delete = fullPermission,
                    Create = fullPermission,
                    GroupID = value.ID,
                    FeatureID = new Guid("{A3D1A5DC-31E9-4D0C-9588-819EC00570FA}")
                };
                if (m_Type.Update().Value)
                {
                }

                //
                m_Type.Value = new Caicho.AccessRight
                {
                    View = true,
                    Edit = fullPermission,
                    Delete = fullPermission,
                    Create = fullPermission,
                    GroupID = value.ID,
                    FeatureID = new Guid("{F145DE00-C062-4596-B386-98B6F3290A29}")
                };
                if (m_Type.Update().Value)
                {
                }

                //
                m_Type.Value = new Caicho.AccessRight
                {
                    View = true,
                    Edit = fullPermission,
                    Delete = fullPermission,
                    Create = fullPermission,
                    GroupID = value.ID,
                    FeatureID = new Guid("{40CCFA26-7042-474D-8BEB-104B9C1801AA}")
                };
                if (m_Type.Update().Value)
                {
                }
                //
                m_Type.Value = new Caicho.AccessRight
                {
                    View = true,
                    Edit = fullPermission,
                    Delete = fullPermission,
                    Create = fullPermission,
                    GroupID = value.ID,
                    FeatureID = new Guid("{5D63C94C-EFDF-41EE-B3E8-ADDF6976EEE1}")
                };
                if (m_Type.Update().Value)
                {
                }

                //
                m_Type.Value = new Caicho.AccessRight
                {
                    View = true,
                    Edit = fullPermission,
                    Delete = false,
                    Create = fullPermission,
                    GroupID = value.ID,
                    FeatureID = new Guid("{AEB831D6-CEEC-4DB3-9695-105B4221FB08}")
                };
                if (m_Type.Update().Value)
                {
                }
                //
                m_Type.Value = new Caicho.AccessRight
                {
                    View = true,
                    Edit = fullPermission,
                    Delete = fullPermission,
                    Create = fullPermission,
                    GroupID = value.ID,
                    FeatureID = new Guid("{2F67B77F-C7F5-4A82-9545-C65E9ACAC280}")
                };
                if (m_Type.Update().Value)
                {
                }

                //
                m_Type.Value = new Caicho.AccessRight
                {
                    View = true,
                    Edit = fullPermission,
                    Delete = fullPermission,
                    Create = fullPermission,
                    GroupID = value.ID,
                    FeatureID = new Guid("{FEC21D99-7E3A-4F1B-969E-B97624C62EB2}")
                };
                if (m_Type.Update().Value)
                {
                }
                //
                m_Type.Value = new Caicho.AccessRight
                {
                    View = true,
                    Edit = fullPermission,
                    Delete = fullPermission,
                    Create = fullPermission,
                    GroupID = value.ID,
                    FeatureID = new Guid("{94E61E46-AFA6-40DE-B2C2-190EFCD83006}")
                };
                if (m_Type.Update().Value)
                {
                }

                //
                m_Type.Value = new Caicho.AccessRight
                {
                    View = true,
                    Edit = fullPermission,
                    Delete = fullPermission,
                    Create = fullPermission,
                    GroupID = value.ID,
                    FeatureID = new Guid("{9960BDF2-0DBA-4326-889D-2419C087C8C0}")
                };
                if (m_Type.Update().Value)
                {
                }
                //
                m_Type.Value = new Caicho.AccessRight
                {
                    View = true,
                    Edit = fullPermission,
                    Delete = false,
                    Create = fullPermission,
                    GroupID = value.ID,
                    FeatureID = new Guid("{AD669EBD-E0E4-49C6-827E-A1B1205745FB}")
                };
                if (m_Type.Update().Value)
                {
                }

                //
                m_Type.Value = new Caicho.AccessRight
                {
                    View = true,
                    Edit = fullPermission,
                    Delete = false,
                    Create = fullPermission,
                    GroupID = value.ID,
                    FeatureID = new Guid("{DDDF6AA6-9299-46A3-9035-D05F32B85AB4}")
                };
                if (m_Type.Update().Value)
                {
                }
                //
                m_Type.Value = new Caicho.AccessRight
                {
                    View = true,
                    Edit = fullPermission,
                    Delete = false,
                    Create = fullPermission,
                    GroupID = value.ID,
                    FeatureID = new Guid("{AA1EA269-2FDA-48C8-B8E4-3D323F5950DC}")
                };
                if (m_Type.Update().Value)
                {
                }

                //
                m_Type.Value = new Caicho.AccessRight
                {
                    View = true,
                    Edit = fullPermission,
                    Delete = false,
                    Create = fullPermission,
                    GroupID = value.ID,
                    FeatureID = new Guid("{CE4383E1-E314-4952-B228-D0C32FB8194C}")
                };
                if (m_Type.Update().Value)
                {
                }
                //
                m_Type.Value = new Caicho.AccessRight
                {
                    View = true,
                    Edit = fullPermission,
                    Delete = false,
                    Create = fullPermission,
                    GroupID = value.ID,
                    FeatureID = new Guid("{D42B66D2-77F8-42B9-A400-E44AFA40B7A8}")
                };
                if (m_Type.Update().Value)
                {
                }

                //
                m_Type.Value = new Caicho.AccessRight
                {
                    View = true,
                    Edit = fullPermission,
                    Delete = fullPermission,
                    Create = fullPermission,
                    GroupID = value.ID,
                    FeatureID = new Guid("{F529552F-0C83-45C6-B32A-D5DF94631A21}")
                };
                if (m_Type.Update().Value)
                {
                }
                //
                m_Type.Value = new Caicho.AccessRight
                {
                    View = true,
                    Edit = fullPermission,
                    Delete = fullPermission,
                    Create = fullPermission,
                    GroupID = value.ID,
                    FeatureID = new Guid("{1BB6EB27-F90B-4AE2-ADD9-3E8E778BA3A9}")
                };
                if (m_Type.Update().Value)
                {
                }

                //
                m_Type.Value = new Caicho.AccessRight
                {
                    View = true,
                    Edit = fullPermission,
                    Delete = false,
                    Create = fullPermission,
                    GroupID = value.ID,
                    FeatureID = new Guid("{D6EDCF06-4195-41E9-B3E4-A9DA28DE5F64}")
                };
                if (m_Type.Update().Value)
                {
                }
                //
                m_Type.Value = new Caicho.AccessRight
                {
                    View = true,
                    Edit = fullPermission,
                    Delete = false,
                    Create = fullPermission,
                    GroupID = value.ID,
                    FeatureID = new Guid("{99EBA815-992A-4F41-B4A1-F6295DBA63FF}")
                };
                if (m_Type.Update().Value)
                {
                }

                //
                m_Type.Value = new Caicho.AccessRight
                {
                    View = true,
                    Edit = fullPermission,
                    Delete = fullPermission,
                    Create = fullPermission,
                    GroupID = value.ID,
                    FeatureID = new Guid("{8DA30E3A-B766-440A-A1E5-41C6A4144636}")
                };
                if (m_Type.Update().Value)
                {
                }

                //
                m_Type.Value = new Caicho.AccessRight
                {
                    View = true,
                    Edit = fullPermission,
                    Delete = fullPermission,
                    Create = fullPermission,
                    GroupID = value.ID,
                    FeatureID = new Guid("{5561D3EB-4671-4BD9-918D-467B914AE1BE}")
                };
                if (m_Type.Update().Value)
                {
                }
            }

            return m_Ret;
        }

        private void TakeOutAccessRight(bool fullPermission)
        {
            var m_Type = new Crm.Setting.AccessRight(this.ConnectionString);

            //
            m_Type.Value = new Caicho.AccessRight
            {
                View = true,
                Edit = fullPermission,
                Delete = fullPermission,
                Create = true,
                GroupID = value.ID,
                FeatureID = new Guid("{47B22CC7-2554-4175-B8E4-E030A3AB8208}")
            };
            if (m_Type.Update().Value)
            {
            }

            //
            m_Type.Value = new Caicho.AccessRight
            {
                View = true,
                Edit = fullPermission,
                Delete = fullPermission,
                Create = fullPermission,
                GroupID = value.ID,
                FeatureID = new Guid("{FBC8106C-421B-4103-82B8-731618C495B9}")
            };
            if (m_Type.Update().Value)
            {
            }

            //warranty
            m_Type.Value = new Caicho.AccessRight
            {
                View = true,
                Edit = fullPermission,
                Delete = fullPermission,
                Create = fullPermission,
                GroupID = value.ID,
                FeatureID = new Guid("{573575D7-0AC7-42A4-8A9E-9CC3C9C157FD}")
            };
            if (m_Type.Update().Value)
            {
            }

            //warranty return
            m_Type.Value = new Caicho.AccessRight
            {
                View = true,
                Edit = fullPermission,
                Delete = fullPermission,
                Create = fullPermission,
                GroupID = value.ID,
                FeatureID = new Guid("{1D840986-887F-4219-84CD-10521C96F6E5}")
            };
            if (m_Type.Update().Value)
            {
            }

            //guarantee
            m_Type.Value = new Caicho.AccessRight
            {
                View = true,
                Edit = fullPermission,
                Delete = fullPermission,
                Create = fullPermission,
                GroupID = value.ID,
                FeatureID = new Guid("{2F6DADD6-59BF-4F93-8FB7-500BE9975645}")
            };
            if (m_Type.Update().Value)
            {
            }

            //guarantee back
            m_Type.Value = new Caicho.AccessRight
            {
                View = true,
                Edit = fullPermission,
                Delete = fullPermission,
                Create = fullPermission,
                GroupID = value.ID,
                FeatureID = new Guid("{95050CE7-6289-4E98-90AE-584843E980B3}")
            };
            if (m_Type.Update().Value)
            {
            }

            //
            m_Type.Value = new Caicho.AccessRight
            {
                View = true,
                Edit = fullPermission,
                Delete = fullPermission,
                Create = fullPermission,
                GroupID = value.ID,
                FeatureID = new Guid("{CD4656D9-969D-4108-B7BE-4BC2CD60C50D}")
            };
            if (m_Type.Update().Value)
            {
            }
            //
            m_Type.Value = new Caicho.AccessRight
            {
                View = true,
                Edit = fullPermission,
                Delete = fullPermission,
                Create = fullPermission,
                GroupID = value.ID,
                FeatureID = new Guid("{942495DE-C985-4410-8372-BA4464B35C51}")
            };
            if (m_Type.Update().Value)
            {
            }

            //
            m_Type.Value = new Caicho.AccessRight
            {
                View = true,
                Edit = fullPermission,
                Delete = fullPermission,
                Create = fullPermission,
                GroupID = value.ID,
                FeatureID = new Guid("{65EE223A-5ED1-4345-B826-12A765555B17}")
            };
            if (m_Type.Update().Value)
            {
            }
            //
            m_Type.Value = new Caicho.AccessRight
            {
                View = true,
                Edit = fullPermission,
                Delete = fullPermission,
                Create = fullPermission,
                GroupID = value.ID,
                FeatureID = new Guid("{06D83FDC-8B5B-4743-A3F6-15E2ED61F413}")
            };
            if (m_Type.Update().Value)
            {
            }

            //
            m_Type.Value = new Caicho.AccessRight
            {
                View = true,
                Edit = fullPermission,
                Delete = fullPermission,
                Create = fullPermission,
                GroupID = value.ID,
                FeatureID = new Guid("{5A291D4B-75AA-411F-BDE9-40D42431C2B0}")
            };
            if (m_Type.Update().Value)
            {
            }
            //
            m_Type.Value = new Caicho.AccessRight
            {
                View = true,
                Edit = fullPermission,
                Delete = fullPermission,
                Create = fullPermission,
                GroupID = value.ID,
                FeatureID = new Guid("{BCF126B2-564A-4DA0-A1BD-FAA16F517C84}")
            };
            if (m_Type.Update().Value)
            {
            }

        }

        private void TakeInAccessRight(bool fullPermission)
        {
            var m_Type = new Crm.Setting.AccessRight(this.ConnectionString);

            //
            m_Type.Value = new Caicho.AccessRight
            {
                View = true,
                Edit = fullPermission,
                Delete = fullPermission,
                Create = fullPermission,
                GroupID = value.ID,
                FeatureID = new Guid("{272E08FD-3F19-4AF0-A052-F50375F5D67D}")
            };
            if (m_Type.Update().Value)
            {
            }

            //
            m_Type.Value = new Caicho.AccessRight
            {
                View = true,
                Edit = fullPermission,
                Delete = fullPermission,
                Create = fullPermission,
                GroupID = value.ID,
                FeatureID = new Guid("{FF160310-504E-4EF2-A112-65034872BE02}")
            };
            if (m_Type.Update().Value)
            {
            }

            //
            m_Type.Value = new Caicho.AccessRight
            {
                View = true,
                Edit = fullPermission,
                Delete = fullPermission,
                Create = fullPermission,
                GroupID = value.ID,
                FeatureID = new Guid("{7847326E-F591-4970-9C55-2093B98FF5A0}")
            };
            if (m_Type.Update().Value)
            {
            }
            //
            m_Type.Value = new Caicho.AccessRight
            {
                View = true,
                Edit = fullPermission,
                Delete = fullPermission,
                Create = fullPermission,
                GroupID = value.ID,
                FeatureID = new Guid("{ADE95E2E-9028-45DB-91D9-8B70BE29ED28}")
            };
            if (m_Type.Update().Value)
            {
            }

            //
            m_Type.Value = new Caicho.AccessRight
            {
                View = true,
                Edit = fullPermission,
                Delete = fullPermission,
                Create = fullPermission,
                GroupID = value.ID,
                FeatureID = new Guid("{8BCD2A3F-91EB-499C-936A-62D584C1A16A}")
            };
            if (m_Type.Update().Value)
            {
            }
            //
            m_Type.Value = new Caicho.AccessRight
            {
                View = true,
                Edit = fullPermission,
                Delete = fullPermission,
                Create = fullPermission,
                GroupID = value.ID,
                FeatureID = new Guid("{3537C9DB-EF2C-4597-8C9A-078C2FF0E5BB}")
            };
            if (m_Type.Update().Value)
            {
            }
        }

        private void InventoryAccessRight(bool fullPermission)
        {
            var m_Type = new Crm.Setting.AccessRight(this.ConnectionString);

            //
            m_Type.Value = new Caicho.AccessRight
            {
                View = true,
                Edit = fullPermission,
                Delete = fullPermission,
                Create = fullPermission,
                GroupID = value.ID,
                FeatureID = new Guid("{4CE82782-D9EE-4024-BC6A-9AF4A2B3DA7E}")
            };
            if (m_Type.Update().Value)
            {
            }

            //
            m_Type.Value = new Caicho.AccessRight
            {
                View = true,
                Edit = fullPermission,
                Delete = fullPermission,
                Create = fullPermission,
                GroupID = value.ID,
                FeatureID = new Guid("{02D378F7-FED5-4555-BDB8-D727328F20F5}")
            };
            if (m_Type.Update().Value)
            {
            }

            //
            m_Type.Value = new Caicho.AccessRight
            {
                View = true,
                Edit = fullPermission,
                Delete = fullPermission,
                Create = fullPermission,
                GroupID = value.ID,
                FeatureID = new Guid("{48B4BF7C-1651-4085-BB00-4C563E004267}")
            };
            if (m_Type.Update().Value)
            {
            }
            //
            m_Type.Value = new Caicho.AccessRight
            {
                View = true,
                Edit = fullPermission,
                Delete = fullPermission,
                Create = fullPermission,
                GroupID = value.ID,
                FeatureID = new Guid("{9E6A0441-A324-452B-918D-BEC3F280F6EF}")
            };
            if (m_Type.Update().Value)
            {
            }

            //
            m_Type.Value = new Caicho.AccessRight
            {
                View = true,
                Edit = fullPermission,
                Delete = fullPermission,
                Create = fullPermission,
                GroupID = value.ID,
                FeatureID = new Guid("{E90321F0-F40E-49C8-805C-046E2699CD4F}")
            };
            if (m_Type.Update().Value)
            {
            }
            //
            m_Type.Value = new Caicho.AccessRight
            {
                View = true,
                Edit = fullPermission,
                Delete = fullPermission,
                Create = fullPermission,
                GroupID = value.ID,
                FeatureID = new Guid("{58D75277-8D56-4B7E-8118-EBD465FDE653}")
            };
            if (m_Type.Update().Value)
            {
            }
            //
            m_Type.Value = new Caicho.AccessRight
            {
                View = true,
                Edit = fullPermission,
                Delete = fullPermission,
                Create = fullPermission,
                GroupID = value.ID,
                FeatureID = new Guid("{BD8A5FCA-DF8F-4791-BCA0-E8160DABCA2D}")
            };
            if (m_Type.Update().Value)
            {
            }
        }

        private void FundsAccessRight(bool fullPermission)
        {
            var m_Type = new Crm.Setting.AccessRight(this.ConnectionString);

            //
            m_Type.Value = new Caicho.AccessRight
            {
                View = true,
                Edit = fullPermission,
                Delete = fullPermission,
                Create = fullPermission,
                GroupID = value.ID,
                FeatureID = new Guid("{B1BF6255-2C70-4422-9518-1021F94AD652}")
            };
            if (m_Type.Update().Value)
            {
            }

            //
            m_Type.Value = new Caicho.AccessRight
            {
                View = true,
                Edit = fullPermission,
                Delete = fullPermission,
                Create = fullPermission,
                GroupID = value.ID,
                FeatureID = new Guid("{0CE56AE3-D70B-4E48-873C-260328A220A6}")
            };
            if (m_Type.Update().Value)
            {
            }

            //
            m_Type.Value = new Caicho.AccessRight
            {
                View = true,
                Edit = fullPermission,
                Delete = fullPermission,
                Create = fullPermission,
                GroupID = value.ID,
                FeatureID = new Guid("{E6927901-6FE2-4BE0-A727-BCA64C9008B3}")
            };
            if (m_Type.Update().Value)
            {
            }
            //
            m_Type.Value = new Caicho.AccessRight
            {
                View = true,
                Edit = fullPermission,
                Delete = fullPermission,
                Create = fullPermission,
                GroupID = value.ID,
                FeatureID = new Guid("{6BE57BF3-A88E-497B-86CE-83BF86B59683}")
            };
            if (m_Type.Update().Value)
            {
            }

        }
    }
}
