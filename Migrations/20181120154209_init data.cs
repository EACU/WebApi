using Microsoft.EntityFrameworkCore.Migrations;

namespace EACAAPI.Migrations
{
    public partial class initdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Faculties",
                columns: new[] { "Id", "Name" },
                values: new object[] { "e7d6edc5-9927-45c8-8b78-ecbf629a31c8", "Творческих индустрий" });

            migrationBuilder.InsertData(
                table: "PlacesInfo",
                columns: new[] { "Id", "CostSemestr", "MainContestPlaces", "NotBudetPlaces", "SpecialQuotaPlaces", "TargetPlaces" },
                values: new object[,]
                {
                    { "516b57e6-a922-4b19-b774-18f70aeef978", null, 10, 6, 2, 0 },
                    { "80eee3d9-3af9-4b95-b24d-9c05734e9bd6", null, 10, 6, 2, 0 },
                    { "55a2159e-a762-40c4-a285-711c6a7176be", null, 10, 8, 2, 2 },
                    { "7f506e9f-e9cd-42c8-84ad-39a54a6741b4", null, 9, 10, 1, 8 },
                    { "46cc84c2-4de6-41c8-9835-85fffa13a757", null, 9, 5, 1, 0 },
                    { "ced5f569-7f68-4cf6-8e02-ee369e47fb5d", null, 9, 5, 1, 0 },
                    { "e736ff71-b872-4be0-bde2-dc1b67f72019", null, 9, 20, 1, 0 },
                    { "49bfab10-7bf7-4488-b579-99b0f0dac132", null, 9, 20, 1, 0 }
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Code", "FacultyId", "Name" },
                values: new object[] { "1f1481fa-60d4-41f8-bb54-f3bcb1d0a4a2", "50.03.01", "e7d6edc5-9927-45c8-8b78-ecbf629a31c8", "Искусства и гуманитарные науки" });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Code", "FacultyId", "Name" },
                values: new object[] { "9a50f83e-6001-47e4-be57-8fb255d78ecb", "09.03.03", "e7d6edc5-9927-45c8-8b78-ecbf629a31c8", "Прикладная информатика" });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "DepartmentId", "FormEducation", "Name", "PeriodOfStudy", "PlacesInfoId", "Year" },
                values: new object[,]
                {
                    { "c0701380-179d-4b9d-bdd8-07394deae025", "1f1481fa-60d4-41f8-bb54-f3bcb1d0a4a2", 0, "Визуальные коммуникации", "4 года", "516b57e6-a922-4b19-b774-18f70aeef978", 2018 },
                    { "81cfefe5-8ce6-4228-9559-2e0f70f53ab6", "9a50f83e-6001-47e4-be57-8fb255d78ecb", 0, "Прикладная информатика в социально-культурной сфере", "4 года", "49bfab10-7bf7-4488-b579-99b0f0dac132", 2017 },
                    { "8b525331-ca8b-45a1-a110-b567d1a1790a", "9a50f83e-6001-47e4-be57-8fb255d78ecb", 0, "Цифровое искусство", "4 года", "e736ff71-b872-4be0-bde2-dc1b67f72019", 2018 },
                    { "930ac9b9-b456-4b04-9b35-249069efdf7c", "1f1481fa-60d4-41f8-bb54-f3bcb1d0a4a2", 1, "Арт и спорт маркетинг", "5 лет", "ced5f569-7f68-4cf6-8e02-ee369e47fb5d", 2018 },
                    { "ba397c4f-ffcd-4732-80d2-9341e0594f3d", "1f1481fa-60d4-41f8-bb54-f3bcb1d0a4a2", 0, "Журналистика в области культуры", "4 года", "46cc84c2-4de6-41c8-9835-85fffa13a757", 2015 },
                    { "ffa3af29-24dc-4ca6-a822-227c27b439c0", "1f1481fa-60d4-41f8-bb54-f3bcb1d0a4a2", 0, "Журналистика в области культуры", "4 года", "46cc84c2-4de6-41c8-9835-85fffa13a757", 2016 },
                    { "d104302b-c0b8-4f47-8e0f-b05088462522", "1f1481fa-60d4-41f8-bb54-f3bcb1d0a4a2", 1, "Журналистика в области культуры", "5 лет", "46cc84c2-4de6-41c8-9835-85fffa13a757", 2018 },
                    { "3d3ad6c0-0871-4529-82d6-b1218dbb2639", "1f1481fa-60d4-41f8-bb54-f3bcb1d0a4a2", 1, "Технологии управления в сфере культуры", "5 лет", "7f506e9f-e9cd-42c8-84ad-39a54a6741b4", 2014 },
                    { "efec1fc2-ad6e-4f15-8e43-48d8609e3245", "1f1481fa-60d4-41f8-bb54-f3bcb1d0a4a2", 1, "Технологии управления в сфере культуры", "5 лет", "7f506e9f-e9cd-42c8-84ad-39a54a6741b4", 2015 },
                    { "fa1ac538-aa74-45c9-a12c-0b4ad9a659c3", "1f1481fa-60d4-41f8-bb54-f3bcb1d0a4a2", 1, "Технологии управления в сфере культуры", "5 лет", "7f506e9f-e9cd-42c8-84ad-39a54a6741b4", 2016 },
                    { "93aac0fd-ef11-41d8-9c84-f781cdf5d1cf", "1f1481fa-60d4-41f8-bb54-f3bcb1d0a4a2", 1, "Технологии управления в сфере культуры", "5 лет", "7f506e9f-e9cd-42c8-84ad-39a54a6741b4", 2017 },
                    { "910eb5fb-d821-48af-9361-4c6467c857e9", "9a50f83e-6001-47e4-be57-8fb255d78ecb", 0, "Прикладная информатика в социально-культурной сфере", "4 года", "49bfab10-7bf7-4488-b579-99b0f0dac132", 2016 },
                    { "688e6bda-0ae2-48f4-b954-3cf235bf011e", "1f1481fa-60d4-41f8-bb54-f3bcb1d0a4a2", 1, "Технологии управления в сфере культуры", "5 лет", "7f506e9f-e9cd-42c8-84ad-39a54a6741b4", 2018 },
                    { "ce9b9260-e265-447e-9eab-07dec9b86e79", "1f1481fa-60d4-41f8-bb54-f3bcb1d0a4a2", 0, "Технологии управления в сфере культуры", "4 года", "55a2159e-a762-40c4-a285-711c6a7176be", 2016 },
                    { "c550cdc0-3bb0-4b98-a739-c74e1fec9194", "1f1481fa-60d4-41f8-bb54-f3bcb1d0a4a2", 0, "Технологии управления в сфере культуры", "4 года", "55a2159e-a762-40c4-a285-711c6a7176be", 2017 },
                    { "e094daff-0612-48c1-8fee-05c5576b054f", "1f1481fa-60d4-41f8-bb54-f3bcb1d0a4a2", 0, "Технологии управления в сфере культуры", "4 года", "55a2159e-a762-40c4-a285-711c6a7176be", 2018 },
                    { "9ccab65b-e74b-4ee9-9413-c95817d8ad44", "1f1481fa-60d4-41f8-bb54-f3bcb1d0a4a2", 0, "Танец и современная пластическая культура", "4 года", "80eee3d9-3af9-4b95-b24d-9c05734e9bd6", 2015 },
                    { "4fd90ad1-84d0-4cbc-9ff4-ba8ca2e370ef", "1f1481fa-60d4-41f8-bb54-f3bcb1d0a4a2", 0, "Танец и современная пластическая культура", "4 года", "80eee3d9-3af9-4b95-b24d-9c05734e9bd6", 2016 },
                    { "509ce781-53f4-4102-8eb7-5e49e59cee38", "1f1481fa-60d4-41f8-bb54-f3bcb1d0a4a2", 0, "Танец и современная пластическая культура", "4 года", "80eee3d9-3af9-4b95-b24d-9c05734e9bd6", 2017 },
                    { "93f0e4b2-3722-4aa5-a01c-b10826040298", "1f1481fa-60d4-41f8-bb54-f3bcb1d0a4a2", 0, "Танец и современная пластическая культура", "4 года", "80eee3d9-3af9-4b95-b24d-9c05734e9bd6", 2018 },
                    { "0ffbfbaa-c9f7-4b3d-bd4d-e74d32126e79", "1f1481fa-60d4-41f8-bb54-f3bcb1d0a4a2", 0, "Визуальные коммуникации", "4 года", "516b57e6-a922-4b19-b774-18f70aeef978", 2015 },
                    { "fb819bb4-b74e-419e-b70c-bb8666b6a978", "1f1481fa-60d4-41f8-bb54-f3bcb1d0a4a2", 0, "Визуальные коммуникации", "4 года", "516b57e6-a922-4b19-b774-18f70aeef978", 2016 },
                    { "9c29a9ca-6860-4e66-b9a6-b341bf67d620", "1f1481fa-60d4-41f8-bb54-f3bcb1d0a4a2", 0, "Визуальные коммуникации", "4 года", "516b57e6-a922-4b19-b774-18f70aeef978", 2017 },
                    { "b536a032-adf8-4dc0-af15-3967407197f7", "1f1481fa-60d4-41f8-bb54-f3bcb1d0a4a2", 0, "Технологии управления в сфере культуры", "4 года", "55a2159e-a762-40c4-a285-711c6a7176be", 2015 },
                    { "5c270abb-09b6-445d-8072-07ca6517748a", "9a50f83e-6001-47e4-be57-8fb255d78ecb", 0, "Прикладная информатика в социально-культурной сфере", "4 года", "49bfab10-7bf7-4488-b579-99b0f0dac132", 2015 }
                });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "Active", "CourseId", "Number" },
                values: new object[,]
                {
                    { "b6c351a5-9a0e-459d-840d-073642c69e4f", true, "c0701380-179d-4b9d-bdd8-07394deae025", 123 },
                    { "58f35aec-000d-414a-9204-165150dc7643", true, "81cfefe5-8ce6-4228-9559-2e0f70f53ab6", 225 },
                    { "922d5911-e3e5-401d-b94f-680843558714", true, "8b525331-ca8b-45a1-a110-b567d1a1790a", 126 },
                    { "89a3e2d0-9652-4545-915c-364d46d25a76", true, "930ac9b9-b456-4b04-9b35-249069efdf7c", 133 },
                    { "9759ee00-07f1-4a99-8328-070d11983c29", true, "ba397c4f-ffcd-4732-80d2-9341e0594f3d", 422 },
                    { "679dd170-705d-46d6-9bd3-06034e5b29e5", true, "ffa3af29-24dc-4ca6-a822-227c27b439c0", 322 },
                    { "6cf90fd1-f616-4e49-8afd-6efc18a4e17f", true, "d104302b-c0b8-4f47-8e0f-b05088462522", 134 },
                    { "9f177c5e-de5c-4a3a-b585-6b708f891174", true, "3d3ad6c0-0871-4529-82d6-b1218dbb2639", 531 },
                    { "aeb21cc7-86ad-43da-b6c8-897a210ee8e1", true, "efec1fc2-ad6e-4f15-8e43-48d8609e3245", 431 },
                    { "fb753244-ea1f-4238-a7cc-69ba8a0640ab", true, "fa1ac538-aa74-45c9-a12c-0b4ad9a659c3", 331 },
                    { "14d9dd23-f28f-40de-b9a7-fb30c80992dc", true, "93aac0fd-ef11-41d8-9c84-f781cdf5d1cf", 231 },
                    { "e38c0ffd-fd30-441c-8ce5-d9131151f7a0", true, "910eb5fb-d821-48af-9361-4c6467c857e9", 325 },
                    { "96af68b1-6365-43d7-b231-24a00837a1cf", true, "688e6bda-0ae2-48f4-b954-3cf235bf011e", 131 },
                    { "d0ed5747-cc20-4e80-be1d-fa04e17ac27c", true, "ce9b9260-e265-447e-9eab-07dec9b86e79", 321 },
                    { "ea664bac-5b98-45b1-ac65-ebc4772005a6", true, "c550cdc0-3bb0-4b98-a739-c74e1fec9194", 221 },
                    { "670804e1-a7a5-4f75-8f4f-e75ea1211057", true, "e094daff-0612-48c1-8fee-05c5576b054f", 121 },
                    { "ec86ea15-b8b8-4868-a92d-552cf68fcb88", true, "9ccab65b-e74b-4ee9-9413-c95817d8ad44", 424 },
                    { "9a2fc5ef-11b9-48cf-af19-55262d5f7ee6", true, "4fd90ad1-84d0-4cbc-9ff4-ba8ca2e370ef", 324 },
                    { "dd805311-58c9-457e-8281-76a64b13a8d6", true, "509ce781-53f4-4102-8eb7-5e49e59cee38", 224 },
                    { "97ae3d71-079b-432c-9e36-944b61f4ca63", true, "93f0e4b2-3722-4aa5-a01c-b10826040298", 124 },
                    { "9afbc88c-31eb-4908-b826-a9c2adcb96a9", true, "0ffbfbaa-c9f7-4b3d-bd4d-e74d32126e79", 423 },
                    { "81bebd5d-426c-4322-adf9-19afb7fc1f3b", true, "fb819bb4-b74e-419e-b70c-bb8666b6a978", 323 },
                    { "9bed449f-0d0e-4efc-bd96-9a256db68950", true, "9c29a9ca-6860-4e66-b9a6-b341bf67d620", 223 },
                    { "a923c3b3-39c9-487c-bf34-aa48a6b2ff12", true, "b536a032-adf8-4dc0-af15-3967407197f7", 421 },
                    { "d9c12db7-31fa-410a-8005-807e863de1de", true, "5c270abb-09b6-445d-8072-07ca6517748a", 425 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "14d9dd23-f28f-40de-b9a7-fb30c80992dc");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "58f35aec-000d-414a-9204-165150dc7643");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "670804e1-a7a5-4f75-8f4f-e75ea1211057");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "679dd170-705d-46d6-9bd3-06034e5b29e5");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "6cf90fd1-f616-4e49-8afd-6efc18a4e17f");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "81bebd5d-426c-4322-adf9-19afb7fc1f3b");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "89a3e2d0-9652-4545-915c-364d46d25a76");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "922d5911-e3e5-401d-b94f-680843558714");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "96af68b1-6365-43d7-b231-24a00837a1cf");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "9759ee00-07f1-4a99-8328-070d11983c29");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "97ae3d71-079b-432c-9e36-944b61f4ca63");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "9a2fc5ef-11b9-48cf-af19-55262d5f7ee6");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "9afbc88c-31eb-4908-b826-a9c2adcb96a9");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "9bed449f-0d0e-4efc-bd96-9a256db68950");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "9f177c5e-de5c-4a3a-b585-6b708f891174");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "a923c3b3-39c9-487c-bf34-aa48a6b2ff12");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "aeb21cc7-86ad-43da-b6c8-897a210ee8e1");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "b6c351a5-9a0e-459d-840d-073642c69e4f");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "d0ed5747-cc20-4e80-be1d-fa04e17ac27c");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "d9c12db7-31fa-410a-8005-807e863de1de");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "dd805311-58c9-457e-8281-76a64b13a8d6");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "e38c0ffd-fd30-441c-8ce5-d9131151f7a0");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "ea664bac-5b98-45b1-ac65-ebc4772005a6");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "ec86ea15-b8b8-4868-a92d-552cf68fcb88");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "fb753244-ea1f-4238-a7cc-69ba8a0640ab");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "0ffbfbaa-c9f7-4b3d-bd4d-e74d32126e79");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "3d3ad6c0-0871-4529-82d6-b1218dbb2639");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "4fd90ad1-84d0-4cbc-9ff4-ba8ca2e370ef");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "509ce781-53f4-4102-8eb7-5e49e59cee38");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "5c270abb-09b6-445d-8072-07ca6517748a");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "688e6bda-0ae2-48f4-b954-3cf235bf011e");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "81cfefe5-8ce6-4228-9559-2e0f70f53ab6");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "8b525331-ca8b-45a1-a110-b567d1a1790a");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "910eb5fb-d821-48af-9361-4c6467c857e9");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "930ac9b9-b456-4b04-9b35-249069efdf7c");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "93aac0fd-ef11-41d8-9c84-f781cdf5d1cf");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "93f0e4b2-3722-4aa5-a01c-b10826040298");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "9c29a9ca-6860-4e66-b9a6-b341bf67d620");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "9ccab65b-e74b-4ee9-9413-c95817d8ad44");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "b536a032-adf8-4dc0-af15-3967407197f7");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "ba397c4f-ffcd-4732-80d2-9341e0594f3d");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "c0701380-179d-4b9d-bdd8-07394deae025");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "c550cdc0-3bb0-4b98-a739-c74e1fec9194");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "ce9b9260-e265-447e-9eab-07dec9b86e79");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "d104302b-c0b8-4f47-8e0f-b05088462522");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "e094daff-0612-48c1-8fee-05c5576b054f");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "efec1fc2-ad6e-4f15-8e43-48d8609e3245");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "fa1ac538-aa74-45c9-a12c-0b4ad9a659c3");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "fb819bb4-b74e-419e-b70c-bb8666b6a978");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "ffa3af29-24dc-4ca6-a822-227c27b439c0");

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: "1f1481fa-60d4-41f8-bb54-f3bcb1d0a4a2");

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: "9a50f83e-6001-47e4-be57-8fb255d78ecb");

            migrationBuilder.DeleteData(
                table: "PlacesInfo",
                keyColumn: "Id",
                keyValue: "46cc84c2-4de6-41c8-9835-85fffa13a757");

            migrationBuilder.DeleteData(
                table: "PlacesInfo",
                keyColumn: "Id",
                keyValue: "49bfab10-7bf7-4488-b579-99b0f0dac132");

            migrationBuilder.DeleteData(
                table: "PlacesInfo",
                keyColumn: "Id",
                keyValue: "516b57e6-a922-4b19-b774-18f70aeef978");

            migrationBuilder.DeleteData(
                table: "PlacesInfo",
                keyColumn: "Id",
                keyValue: "55a2159e-a762-40c4-a285-711c6a7176be");

            migrationBuilder.DeleteData(
                table: "PlacesInfo",
                keyColumn: "Id",
                keyValue: "7f506e9f-e9cd-42c8-84ad-39a54a6741b4");

            migrationBuilder.DeleteData(
                table: "PlacesInfo",
                keyColumn: "Id",
                keyValue: "80eee3d9-3af9-4b95-b24d-9c05734e9bd6");

            migrationBuilder.DeleteData(
                table: "PlacesInfo",
                keyColumn: "Id",
                keyValue: "ced5f569-7f68-4cf6-8e02-ee369e47fb5d");

            migrationBuilder.DeleteData(
                table: "PlacesInfo",
                keyColumn: "Id",
                keyValue: "e736ff71-b872-4be0-bde2-dc1b67f72019");

            migrationBuilder.DeleteData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: "e7d6edc5-9927-45c8-8b78-ecbf629a31c8");
        }
    }
}
